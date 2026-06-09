use sistema_nomina;

CREATE  FUNCTION `existe_aumento`(
  P_AUMENTOS_ID INT,
   P_ID_EMPLEADO INT,
   P_FEC_AUM DATE,
   P_ID_CAT INT
) RETURNS int
    DETERMINISTIC
BEGIN
  declare v_salida int;
  SET P_AUMENTOS_ID = COALESCE(P_AUMENTOS_ID, -1);
    SELECT COUNT(ID_EMPLEADO) INTO v_salida FROM aumentos 
    WHERE 
    AUMENTOS_ID <> P_AUMENTOS_ID 
    AND ID_EMPLEADO =P_ID_EMPLEADO
    AND FECHA= P_FEC_AUM
        AND ID_CATEGORIA = P_ID_CAT;
RETURN v_salida;
END ;




CREATE  FUNCTION `existe_departamento`(
   P_ID INT,
   P_COD_DEP VARCHAR(30),
   P_NOM_DEP VARCHAR(100)
) RETURNS int
    DETERMINISTIC
BEGIN
  declare v_salida int;
  SET P_ID = COALESCE(P_ID, -1);
    SELECT COUNT(ID_DEP) INTO v_salida FROM DEPARTAMENTO 
    WHERE 
    ID_DEP <> P_ID 
    AND (NOM_DEP =  P_NOM_DEP || COD_DEP=P_COD_DEP );
RETURN v_salida;
END ;



CREATE  FUNCTION `existe_prestamo`(
    P_PRESTAMOS_ID INT, 
    P_ID_EMPLEADO INT,
    P_FECHA DATE
) RETURNS int
    READS SQL DATA
    DETERMINISTIC
BEGIN
    DECLARE v_salida INT;
    
    -- Manejo de NULL para el ID
    SET P_PRESTAMOS_ID = COALESCE(P_PRESTAMOS_ID, -1);
    
    -- Lógica corregida: 
    -- Buscamos si hay UN EMPLEADO con la MISMA FECHA pero con un ID DE PRESTAMO DISTINTO
    SELECT COUNT(*) INTO v_salida 
    FROM PRESTAMO 
    WHERE ID_EMPLEADO = P_ID_EMPLEADO
      AND FECHA = P_FECHA
      AND ID_PRESTAMO <> P_PRESTAMOS_ID;

    RETURN v_salida;
END ;



CREATE  FUNCTION `existe_rango_fecha_ausencia`(
  P_AUSENCIAS_ID INT,
  P_ID_EMPLEADO INT,
  P_ID_NOMINA VARCHAR(8),
  P_FECI_AU DATE,
  P_FECF_AU DATE
) RETURNS int
    DETERMINISTIC
BEGIN
  declare salida int;
    
   SET P_AUSENCIAS_ID = COALESCE(P_AUSENCIAS_ID, -1);
   SELECT COUNT(ID_EMPLEADO) into salida FROM ausencias
   WHERE
    AUSENCIAS_ID <> P_AUSENCIAS_ID
    AND ID_EMPLEADO = P_ID_EMPLEADO
        AND (
      FECHA_INICIAL  BETWEEN P_FECI_AU AND P_FECF_AU
        OR FECHA_FINAL  BETWEEN P_FECI_AU AND P_FECF_AU
    );
RETURN salida;
END ;



CREATE  FUNCTION `f_obtener_sueldo`(
  V_ID_EMPLEADO INT
) RETURNS decimal(17,2)
    DETERMINISTIC
begin
      declare v_sueldo decimal(17,2);
    SELECT SUELDO INTO V_SUELDO FROM EMPLEADO WHERE ID_TRB = V_ID_EMPLEADO;
		  
RETURN v_sueldo;
END ;



CREATE  FUNCTION `obtener_sueldo_empleado_ausencia`(
  P_ID_EMPLEADO INT,
  P_NUMERO_DIAS_TRABAJADOS INT
) RETURNS decimal(15,2)
    DETERMINISTIC
BEGIN
	DECLARE suel DECIMAL(17,2);
	SELECT sueldo INTO suel
	FROM empleado
	WHERE ID_TRB = P_ID_EMPLEADO;
		SET suel = (suel/30)*P_NUMERO_DIAS_TRABAJADOS;
RETURN suel;
END ;



CREATE PROCEDURE `acciones_antecedente`(
    IN P_ID_ANTECEDENTE  INT,
	 IN P_NUMERO_ANTECEDENTE INT(32),
	 IN P_FECHA_EMISION DATE,
	 IN  P_FECHA_VENCIMIENTO DATE,
	 IN P_VIGENCIA DATE,
	 IN  P_LUGAR_ORIGEN VARCHAR(100),
     IN P_TIPO_ANTECEDENTE VARCHAR(2),
	 IN P_ACCION VARCHAR(1),
     IN P_ID_EMPLEADO INT,
	 OUT P_SALIDA int
)
BEGIN

  DECLARE MSG VARCHAR(100);
  DECLARE code CHAR(5) DEFAULT '00000';
  DECLARE v_nombre_columna text;
  DECLARE v_valor_campo text;
  DECLARE V_ID_EMPLEADO INT;
  DECLARE V_DEBITO DECIMAL;
  DECLARE V_ACTUAL DECIMAL;
  DECLARE V_CREDITO DECIMAL;
  DECLARE V_ANTERIOR DECIMAL;
 
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    CASE P_ACCION
		 WHEN "N" THEN

				 INSERT INTO ANTECEDENTES(NUMERO_ANTECEDENTE, FECHA_EMISION,
					FECHA_VENCIMIENTO,VIGENCIA, LUGAR_ORIGEN , ID_EMPLEADO,TIPO_ANTECEDENTE
				  )
			     VALUES(P_NUMERO_ANTECEDENTE, P_FECHA_EMISION,
					P_FECHA_VENCIMIENTO,P_VIGENCIA, P_LUGAR_ORIGEN , P_ID_EMPLEADO, P_TIPO_ANTECEDENTE);
				
		   SET P_SALIDA = 1;
     
      WHEN "M" THEN
           UPDATE  ANTECEDENTES
	   SET 
           NUMERO_ANTECEDENTE=P_NUMERO_ANTECEDENTE,
           FECHA_EMISION=P_FECHA_EMISION,
		   FECHA_VENCIMIENTO=P_FECHA_VENCIMIENTO,
		   VIGENCIA=P_VIGENCIA, 
           LUGAR_ORIGEN  =P_LUGAR_ORIGEN,
           TIPO_ANTECEDENTE = P_TIPO_ANTECEDENTE
		  WHERE ID_ANTECEDENTE = P_ID_ANTECEDENTE;
		
	WHEN "E" THEN
           DELETE FROM ANTECEDENTES WHERE ID_ANTECEDENTE = P_ID_ANTECEDENTE; 
   END CASE; 
   
    IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'ANTECEDENTE',code,NOW(),v_nombre_columna,v_valor_campo);
      SET P_SALIDA = -1;
	else
		set P_salida =1;
    END IF;
END;

CREATE  PROCEDURE `acciones_categoria`(
   IN P_ACCION VARCHAR(1),
  IN P_ID_CATEGORIA INT,
  IN P_COD_CAT VARCHAR(3),
  IN P_NOM_CAT VARCHAR(30),
  IN P_SAL_INI DECIMAL(17,2),
  IN P_SAL_FIN DECIMAL(17,2),
  OUT salida INT
)
BEGIN
	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    CASE P_ACCION
		 WHEN "N" THEN
				IF P_ID_CATEGORIA = -1 THEN
				  SET P_ID_CATEGORIA= NULL;
				END IF;
					INSERT INTO categoria (COD_CAT, NOM_CAT,SAL_INI,SAL_FIN)
					VALUES(P_COD_CAT,P_NOM_CAT,P_SAL_INI,P_SAL_FIN);
	
   WHEN "M" THEN
		IF P_ID_CATEGORIA = -1 THEN
			SET P_ID_CATEGORIA= NULL;
		END IF;
		UPDATE categoria
			SET  COD_CAT =P_COD_CAT, 
			NOM_CAT = P_NOM_CAT, 
			SAL_INI = P_SAL_INI ,
			SAL_FIN=P_SAL_FIN  
        WHERE ID_CAT = P_ID_CATEGORIA;
		
  WHEN "E" THEN
           DELETE FROM categoria WHERE ID_CAT = P_ID_CATEGORIA;
    END CASE;
    
	 IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'CATEGORIA',code,NOW(),v_nombre_columna,v_valor_campo);
      SET SALIDA = -1;
	else
		set salida =1;
    END IF;
    
END ;



CREATE  PROCEDURE `acciones_departamento`(
   IN P_ACCION VARCHAR(1),
   IN P_COD_DEP VARCHAR(3),
   IN P_NOM_DEP VARCHAR(30),
   IN P_ID_EMPLEADO int,
   IN P_ID_CUENTA INT,
   IN P_ID_DEP INT,
   OUT SALIDA INT
)
BEGIN 
	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_EXISTE_COD INT DEFAULT 0; -- Nueva variable para la validación

	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
		GET DIAGNOSTICS CONDITION 1
		code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT, v_nombre_columna = column_name;
    END;

	CASE P_ACCION
		 WHEN "N" THEN
				IF P_ID_EMPLEADO = -1 THEN
				   SET P_ID_EMPLEADO = NULL;
				END IF;
				
                -- Validar que el código no exista al crear uno nuevo
                SELECT COUNT(*) INTO V_EXISTE_COD 
                FROM departamento 
                WHERE COD_DEP = P_COD_DEP;
                
                IF V_EXISTE_COD > 0 THEN
					SET code = '45000'; -- Código de error personalizado
                    SET MSG = CONCAT('El código de departamento ', P_COD_DEP, ' ya existe.');
                ELSE
					INSERT INTO departamento (COD_DEP, NOM_DEP, ID_CUENTA, ID_EMPLEADO)
					VALUES (P_COD_DEP, P_NOM_DEP, P_ID_CUENTA, P_ID_EMPLEADO);
                END IF;
				
		 WHEN "M" THEN
				IF P_ID_EMPLEADO = -1 THEN
				   SET P_ID_EMPLEADO = NULL;
				END IF;
                
                -- LA CLAVE: Validar si el código ya existe en OTRO departamento diferente a este
                SELECT COUNT(*) INTO V_EXISTE_COD 
                FROM departamento 
                WHERE COD_DEP = P_COD_DEP AND ID_DEP <> P_ID_DEP;
                
                IF V_EXISTE_COD > 0 THEN
					-- Si el conteo es mayor a 0, significa que otro registro ya está usando ese código
                    SET code = '45000'; -- Forzamos un código de estado de error personalizado
                    SET MSG = CONCAT('No se puede modificar. El código ', P_COD_DEP, ' ya está asignado a otro departamento.');
                ELSE
					-- Si es 0, significa que el código no cambió o cambió a uno disponible.
					UPDATE departamento SET  
						 COD_DEP = P_COD_DEP, 
						 NOM_DEP = P_NOM_DEP,
						 ID_EMPLEADO = P_ID_EMPLEADO,
						 ID_CUENTA = P_ID_CUENTA  
					WHERE ID_DEP = P_ID_DEP;
                END IF;
		
		 WHEN "E" THEN
				DELETE FROM departamento WHERE ID_DEP = P_ID_DEP;
	END CASE;
    
    -- Tu lógica de control de errores se mantiene intacta y capturará el error personalizado
    IF code <> '00000' THEN  
		INSERT INTO error_log (MENSAJE, TABLA, CODIGO_ERROR, FECHA_ERROR, NOMBRE_COLUMNA, VALOR_CAMPO) 
		VALUES (MSG, 'DEPARTAMENTO', code, NOW(), v_nombre_columna, v_valor_campo);
		SET SALIDA = -1;
	ELSE
		SET SALIDA = 1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_descuento`(
   IN P_COD_DEC VARCHAR(3), 
   IN P_NOM_DEC VARCHAR(30), 
   IN P_VAL_DEC DECIMAL(17,2), 
   IN P_FAC_DEC DECIMAL(17,7), 
   IN P_ID_TIPO_JORNADA INT, 
   IN P_ID_TIPO_PAGO INT, 
   IN P_ID_COD_CUE VARCHAR(8),
   IN P_ACCION VARCHAR(1),
   IN P_ID_DEC INT,
   OUT salida INT
)
BEGIN
	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_EXISTE_COD INT DEFAULT 0;

	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
		GET DIAGNOSTICS CONDITION 1
		code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT, v_nombre_columna = column_name;
    END;
    
	CASE P_ACCION
		 WHEN "N" THEN
				-- Validar si ya existe un descuento con el mismo CÓDIGO o NOMBRE
				SELECT COUNT(*) INTO V_EXISTE_COD 
				FROM descuento 
				WHERE COD_DEC = P_COD_DEC OR NOM_DEC = P_NOM_DEC;
				
				IF V_EXISTE_COD > 0 THEN
					SET code = '45000'; -- Forzar código de error personalizado
					SET MSG = 'No se puede guardar. El código o el nombre del descuento ya existe.';
				ELSE
					INSERT INTO descuento (COD_DEC, NOM_DEC, VAL_DEC, FAC_DEC, ID_TIPO_JORNADA, ID_TIPO_PAGO, ID_COD_CUE)
					VALUES (P_COD_DEC, P_NOM_DEC, P_VAL_DEC, P_FAC_DEC, P_ID_TIPO_JORNADA, P_ID_TIPO_PAGO, P_ID_COD_CUE);
				END IF;

         WHEN "M" THEN
				-- Validar si el código o el nombre ya existen en OTRO registro diferente al que se edita
				SELECT COUNT(*) INTO V_EXISTE_COD 
				FROM descuento 
				WHERE (COD_DEC = P_COD_DEC OR NOM_DEC = P_NOM_DEC) AND ID_DESCUENTO <> P_ID_DEC;
				
				IF V_EXISTE_COD > 0 THEN
					SET code = '45000'; -- Forzar código de error personalizado
					SET MSG = 'No se puede modificar. El código o el nombre ya están asignados a otro descuento.';
				ELSE
					UPDATE descuento
					SET  COD_DEC = P_COD_DEC, 
						 NOM_DEC = P_NOM_DEC,
						 VAL_DEC = P_VAL_DEC, 
						 FAC_DEC = P_FAC_DEC, 
						 ID_TIPO_JORNADA = P_ID_TIPO_JORNADA,
						 ID_TIPO_PAGO = P_ID_TIPO_PAGO,
						 ID_COD_CUE = P_ID_COD_CUE
					WHERE ID_DESCUENTO = P_ID_DEC; 	
				END IF;

	     WHEN "E" THEN
			  DELETE FROM descuento WHERE ID_DESCUENTO = P_ID_DEC;
    END CASE;  
    
    -- Lógica de salida y registro en la bitácora de errores
    IF code <> '00000' THEN  
		INSERT INTO error_log (MENSAJE, TABLA, CODIGO_ERROR, FECHA_ERROR, NOMBRE_COLUMNA, VALOR_CAMPO) 
		VALUES (MSG, 'DESCUENTO', code, NOW(), v_nombre_columna, v_valor_campo);
		SET salida = -1;
	ELSE
		SET salida = 1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_empleado`(
	 IN P_COD_TRB VARCHAR(5),
	 IN P_NOM_TRB VARCHAR(50), 
	 IN P_FEC_NAC DATE, 
	 IN P_IDEN_TRB VARCHAR(20),
	 IN P_EST_TRB VARCHAR(1),
	 IN P_PAST_TRB VARCHAR(20),
	 IN P_RTN_TRB VARCHAR(15),
	 IN P_ANT_TRB VARCHAR(8),
	 IN P_IHS_TRB VARCHAR(13), 
	 IN P_DIR_TRB VARCHAR(40),
	 IN P_TEL_TRB VARCHAR(14), 
	 IN P_FEC_DEF DATE,
	 IN P_SEX_TRB VARCHAR(1), 
	 IN P_TIPO_TRB VARCHAR(1),
	 IN P_ID_DEP INT,
	 IN P_ID_CAT INT,
	 IN P_PUEST_TRB VARCHAR(30),
	 IN P_SUELDO DOUBLE(17,2), 
	 IN P_A_IHS VARCHAR(1), 
	 IN P_A_FSV VARCHAR(1), 
	 IN P_A_SIN VARCHAR(1), 
	 IN P_A_ISR VARCHAR(1),
	 IN P_ID_FORMA_PAGO  INT,
	 IN P_BANCOS VARCHAR(15),
	 IN P_NCUENTA VARCHAR(13),
	 IN P_ID INT,
	 IN P_CELULAR_TRB VARCHAR(15),
	 IN  P_RESIDENCIA_TRB VARCHAR(20),
	 IN P_LICENCIA_TRB VARCHAR(15),
	 IN P_ACCION VARCHAR(1),
     IN P_FECHA_INICIO DATE,
     IN P_TIPO_EMPLEADO VARCHAR(1),
     IN P_CUENTA_SUELDO DECIMAL(17,2),
     IN P_CUENTA_SEGURO_SOCIAL DECIMAL(17,2),
     IN P_CUENTA_REGIMEN_ESPECIAL DECIMAL(17,2),
     IN P_CUENTA_ISR DECIMAL(17,2),
     IN P_OTRA_CUENTA_1  DECIMAL(17,2),
     IN P_OTRA_CUENTA_2  DECIMAL(17,2),
	 OUT salida int
)
BEGIN
	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_ID_EMPLEADO INT;
    
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
       GET DIAGNOSTICS CONDITION 1
       code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT, v_nombre_columna = column_name;
    END;
    
    CASE P_ACCION
		 WHEN "N" THEN
			 INSERT INTO empleado (COD_TRB,NOM_TRB, FEC_NAC, IDEN_TRB , EST_TRB,
				PASAPORTE, RTN,ANTECEDENTES, IHS, DIRECCION,
				TELEFONO, FEC_DEF , SEXO , ID_TIPO_EMPLEADO, ID_DEP, ID_CAT,
				 PUESTO_TRABAJO, SUELDO, AFECTA_IHS, AFECTA_FSV,AFECTA_SIN, AFECTA_ISR,
                 ID_TIPO_PAGO,BANCOS,NCUENTA,
				CELULAR,RESIDENCIA,LICENCIA,FECHA_INICIO,TIPO_EMPLEADO,
                CUENTA_SUELDO,CUENTA_SEGURO_SOCIAL,CUENTA_REGIMEN_ESPECIAL,
                CUENTA_ISR,OTRA_CUENTA_1,OTRA_CUENTA_2, FECHA_CONTRATACION
              )
		   VALUES(P_COD_TRB,P_NOM_TRB, P_FEC_NAC, P_IDEN_TRB , P_EST_TRB,
					P_PAST_TRB, P_RTN_TRB,P_ANT_TRB, P_IHS_TRB, P_DIR_TRB,
					P_TEL_TRB, P_FEC_DEF , P_SEX_TRB , P_TIPO_TRB, P_ID_DEP, P_ID_CAT,
					P_PUEST_TRB, P_SUELDO, P_A_IHS, P_A_FSV,P_A_SIN, P_A_ISR,P_ID_FORMA_PAGO,
					P_BANCOS,P_NCUENTA,P_CELULAR_TRB,P_RESIDENCIA_TRB,
					P_LICENCIA_TRB,P_FECHA_INICIO,P_TIPO_EMPLEADO,P_CUENTA_SUELDO,
                    P_CUENTA_SEGURO_SOCIAL,P_CUENTA_REGIMEN_ESPECIAL,
                    P_CUENTA_ISR,P_OTRA_CUENTA_1,P_OTRA_CUENTA_2,P_FEC_DEF
                    );
			
             IF code = '00000' THEN
				 SET V_ID_EMPLEADO = LAST_INSERT_ID();

				 CALL llenar_tabla_historial_sueldo_empleado(V_ID_EMPLEADO, P_FEC_DEF);
             END IF;
	  
		 WHEN "M" THEN
			UPDATE empleado
			SET 
			COD_TRB =P_COD_TRB, NOM_TRB=P_NOM_TRB, FEC_NAC=P_FEC_NAC, IDEN_TRB =P_IDEN_TRB, EST_TRB =P_EST_TRB,
			PASAPORTE = P_PAST_TRB, RTN=P_RTN_TRB, ANTECEDENTES = P_ANT_TRB, IHS=P_IHS_TRB, DIRECCION=P_DIR_TRB,
			TELEFONO=P_TEL_TRB, FEC_DEF=P_FEC_DEF, SEXO=P_SEX_TRB , ID_TIPO_EMPLEADO=P_TIPO_TRB, ID_DEP=P_ID_DEP, 
			ID_CAT=P_ID_CAT, PUESTO_TRABAJO=P_PUEST_TRB, SUELDO=P_SUELDO, AFECTA_IHS=P_A_IHS, AFECTA_FSV=P_A_FSV,
			AFECTA_SIN=P_A_SIN, AFECTA_ISR = P_A_ISR , ID_TIPO_PAGO = P_ID_FORMA_PAGO, BANCOS = P_BANCOS,
			NCUENTA = P_NCUENTA, CELULAR = P_CELULAR_TRB, RESIDENCIA =P_RESIDENCIA_TRB, LICENCIA=P_LICENCIA_TRB,
			FECHA_INICIO=P_FECHA_INICIO, TIPO_EMPLEADO = P_TIPO_EMPLEADO, CUENTA_SUELDO = P_CUENTA_SUELDO,
			CUENTA_SEGURO_SOCIAL=P_CUENTA_SEGURO_SOCIAL, CUENTA_REGIMEN_ESPECIAL=P_CUENTA_REGIMEN_ESPECIAL, 
			CUENTA_ISR=P_CUENTA_ISR, OTRA_CUENTA_1=P_OTRA_CUENTA_1, OTRA_CUENTA_2=P_OTRA_CUENTA_2,
            FECHA_CONTRATACION = P_FEC_DEF
			WHERE ID_TRB = P_ID;
		
		 WHEN "E" THEN
			DELETE FROM empleado WHERE ID_TRB = P_ID; 
    END CASE; 
   
    IF code <> '00000' THEN  
	   INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
       VALUES (MSG,'EMPLEADO',code,NOW(),v_nombre_columna,v_valor_campo);
       SET SALIDA = -1;
	ELSE
	   SET SALIDA = 1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_labor`(
	 IN P_ID_LAB INT,
     IN P_COD_LAB VARCHAR(3),
	 IN P_NOM_LAB VARCHAR(30),
	 IN P_TIPO_JORNADA VARCHAR(1),
	 IN P_VAL_LAB DOUBLE(17,2),
	 IN P_FAC_LAB DOUBLE(17,7),
	 IN P_ID_TIPO_PAGO INT, 
	 IN P_ID_CUENTA INT(8),
     IN P_ACCION VARCHAR(1),
     OUT salida int
)
BEGIN

	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    CASE P_ACCION
		 WHEN "N" THEN
            INSERT INTO labores(COD_LAB,NOM_LAB,ID_TIPO_JORNADA,
              VAL_LAB,FAC_LAB,ID_TIPO_PAGO,ID_CUENTA)
           VALUES (P_COD_LAB , P_NOM_LAB, P_TIPO_JORNADA,
                P_VAL_LAB, P_FAC_LAB, P_ID_TIPO_PAGO , P_ID_CUENTA);
         WHEN "M" THEN
			UPDATE labores SET
            COD_LAB = P_COD_LAB , 
            NOM_LAB =  P_NOM_LAB, 
            ID_TIPO_JORNADA = P_TIPO_JORNADA,
            VAL_LAB = P_VAL_LAB, 
            FAC_LAB= P_FAC_LAB , 
            ID_TIPO_PAGO = P_ID_TIPO_PAGO,
	        ID_CUENTA = P_ID_CUENTA
      WHERE  ID_LAB = P_ID_LAB; 
         WHEN "E" THEN
             DELETE FROM labores 
              WHERE ID_LAB  = P_ID_LAB ;
    END CASE;     
     
      IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'LABOR',code,NOW(),v_nombre_columna,v_valor_campo);
      SET SALIDA = -1;
	else
		set salida =1;
    END IF;
    
 END ;



CREATE  PROCEDURE `acciones_maumentos`(
 IN P_ACCION VARCHAR(1),
 IN P_ID_AUMENTO INT,
 IN P_ID_EMPLEADO INT,
 IN P_ID_CATEGORIA INT,
 IN P_FECHA DATE,
 IN P_SUELDO_ANTERIOR DECIMAL(17,2),
 IN P_SUELDO_ACTUAL DECIMAL(17,2),
 IN P_TIPO_AUMENTO_ID INT,
 IN P_PORCENTAJE DECIMAL(13,3),
 IN P_MONTO DECIMAL(17,2),
 IN P_TOTAL_MONTO DECIMAL(17,2),
 IN P_DESCRIPCION VARCHAR(30),
 OUT P_SALIDA INT
)
BEGIN
   DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_ID_EMPLEADO INT;
    
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    
     CASE P_ACCION
		 WHEN "N" THEN
			if existe_aumento( NULL,P_ID_EMPLEADO,P_FECHA,P_ID_CATEGORIA ) = 0 THEN
				 INSERT INTO aumentos (ID_EMPLEADO, FECHA, 
						  ID_CATEGORIA, SUELDO_ANTERIOR,
						  SUELDO_ACTUAL, TIPO_AUMENTO_ID,
						  PORCENTAJE, MONTO,TOTAL_MONTO ,DESCRIPCION)
					VALUES (P_ID_EMPLEADO, P_FECHA, 
						  P_ID_CATEGORIA, P_SUELDO_ANTERIOR,
						  P_SUELDO_ACTUAL, P_TIPO_AUMENTO_ID,
						  P_PORCENTAJE, P_MONTO,P_TOTAL_MONTO ,P_DESCRIPCION);
				 
				 UPDATE empleado SET SUELDO = P_SUELDO_ACTUAL 
				 WHERE ID_TRB = P_ID_EMPLEADO;
				 
				 INSERT INTO historial_aumento (ID_EMPLEADO,FECHA,SUELDO_ANTERIOR,SUELDO_ACTUAL,MONTO, ID_CAT)
							VALUES(P_ID_EMPLEADO,P_FECHA, P_SUELDO_ANTERIOR,P_SUELDO_ACTUAL,P_MONTO, P_ID_CATEGORIA);
							
				 CALL llenar_tabla_historial_sueldo_empleado(P_ID_EMPLEADO,P_FECHA);			
				   SET P_SALIDA = 1;
	    ELSE 
			SET P_SALIDA = 0;
	    END IF;
         
         WHEN "M" THEN
         if existe_aumento( P_ID_AUMENTO,P_ID_EMPLEADO,P_FECHA,P_ID_CATEGORIA ) = 0 THEN
			 UPDATE AUMENTOS
              SET
			  ID_EMPLEADO=P_ID_EMPLEADO, 
			  FECHA=P_FECHA, 
			  ID_CATEGORIA=P_ID_CATEGORIA, 
			  SUELDO_ANTERIOR=P_SUELDO_ANTERIOR,
			  SUELDO_ACTUAL=P_SUELDO_ACTUAL, 
			  TIPO_AUMENTO_ID=P_TIPO_AUMENTO_ID,
			  PORCENTAJE=P_PORCENTAJE, 
			  MONTO=P_MONTO,
			  TOTAL_MONTO=P_TOTAL_MONTO ,
			  DESCRIPCION=P_DESCRIPCION
	        WHERE AUMENTOS_ID = P_ID_AUMENTO;
		    
           UPDATE historial_aumento SET
					 FECHA = P_FECHA,
					 SUELDO_ANTERIOR = P_SUELDO_ANTERIOR,
					 SUELDO_ACTUAL = P_SUELDO_ACTUAL,
					 MONTO =P_MONTO
			    WHERE ID_EMPLEADO = P_ID_EMPLEADO
                   AND ID_CAT= P_ID_CATEGORIA
                   AND FECHA = P_FECHA_ANTIGUA;
          SET P_SALIDA = 1;
        ELSE
          SET P_SALIDA= 0;
		END IF;
        
         
         WHEN "E" THEN
           DELETE FROM AUMENTOS WHERE  AUMENTOS_ID = P_ID_AUMENTO;
          
      END CASE; 
      
	IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'AUMENTOS',code,NOW(),v_nombre_columna,v_valor_campo);
      SET P_SALIDA = -1;
	else
		set P_SALIDA =1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_mausencias`(
   IN P_ACCION VARCHAR(1),
   IN P_ID_AUSENCIA INT,
   IN P_ID_EMPLEADO INT,
   IN P_ID_TIPO_AUSENCIA INT,
   IN P_FEC_INICIAL_AU DATE,
   IN P_FEC_FINAL_AU DATE,
   IN P_ID_NOMINA VARCHAR(8),
   IN P_SEPTIMO VARCHAR(1),
   IN P_MONTO DECIMAL(17,2),
   OUT salida INT
)
BEGIN
   declare dias_faltados int;
  declare sueldo decimal(17,2);
   
  DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_ID_EMPLEADO INT;
    
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
     set salida = 0;
 
    
     CASE P_ACCION
		 WHEN "N" THEN
			IF(P_FEC_FINAL_AU = '1/1/0001') THEN
			   set dias_faltados = 1;
			   set sueldo = obtener_sueldo_empleado_ausencia( P_ID_EMPLEADO,dias_faltados);
			   INSERT INTO ausencias (
			   ID_EMPLEADO,
			   ID_TIPO_AUSENCIA,
			   FECHA_INICIAL,
			   FECHA_FINAL,
			   NUMERO_DIAS_TRABAJADOS,
			   MONTO,
			   ID_NOMINA,
			   SEPTIMO
               ) VALUES(
						  P_ID_EMPLEADO,
						  P_ID_TIPO_AUSENCIA,
						 P_FEC_INICIAL_AU,
						 P_FEC_FINAL_AU,
						 dias_faltados,
						 sueldo,
						 P_ID_NOMINA,
						 P_SEPTIMO
                         );
			  set salida = 1;
		  ELSEIF(existe_rango_fecha_ausencia(NULL,P_ID_EMPLEADO,P_ID_NOMINA,P_FEC_INICIAL_AU,P_FEC_FINAL_AU) = 0) THEN
			  
			  SELECT TIMESTAMPDIFF(DAY, P_FEC_INICIAL_AU,P_FEC_FINAL_AU) into dias_faltados;
			  SET dias_faltados = dias_faltados+1;
			  set sueldo = obtener_sueldo_empleado_ausencia(P_ID_EMPLEADO,dias_faltados);
			  INSERT INTO ausencias (
				 ID_EMPLEADO,
			     ID_TIPO_AUSENCIA,
				  FECHA_INICIAL,
		          FECHA_FINAL,
			     NUMERO_DIAS_TRABAJADOS,
			    MONTO,
			    ID_NOMINA,
		        SEPTIMO
              ) VALUES(
					  P_ID_EMPLEADO,
					  P_ID_TIPO_AUSENCIA,
					 P_FEC_INICIAL_AU,
					 P_FEC_FINAL_AU,
					 dias_faltados,
					 sueldo,
					 P_ID_NOMINA,
					 P_SEPTIMO);
			set salida = 1;
         END IF;
      
         WHEN "M" THEN
           IF(P_FEC_FINAL_AU = '1/1/0001') THEN
             set dias_faltados = 1;
             set sueldo = obtener_sueldo_empleado_ausencia( P_ID_EMPLEADO,dias_faltados);
             UPDATE ausencias SET
			       ID_EMPLEADO= P_ID_EMPLEADO,
				   ID_TIPO_AUSENCIA=P_ID_TIPO_AUSENCIA,
				   FECHA_INICIAL=	P_FEC_INICIAL_AU,
				   FECHA_FINAL=	P_FEC_FINAL_AU,
				   NUMERO_DIAS_TRABAJADOS=dias_faltados,
				   MONTO=	sueldo,
				   ID_NOMINA=	P_ID_NOMINA,
				   SEPTIMO =	P_SEPTIMO
                  
			WHERE AUSENCIAS_ID = P_ID_AUSENCIA/*COD_TRB = _COD_TRB AND
				FECI_AU = FECHA_ANTIGUA  AND
				COD_NOM = _COD_NOM*/;
			 set salida = 1;
		  ELSEIF(existe_rango_fecha_ausencia(P_ID_AUSENCIA,P_ID_EMPLEADO,P_ID_NOMINA,P_FEC_INICIAL_AU,	P_FEC_FINAL_AU) = 0) THEN
		  
			SELECT TIMESTAMPDIFF(DAY,P_FEC_INICIAL_AU, P_FEC_FINAL_AU) into dias_faltados;
			  SET dias_faltados = dias_faltados+1;
			  SET sueldo = obtener_sueldo_empleado_ausencia( P_ID_EMPLEADO,dias_faltados);
			  
			 UPDATE ausencias SET
			     ID_EMPLEADO= P_ID_EMPLEADO,
				   ID_TIPO_AUSENCIA=P_ID_TIPO_AUSENCIA,
				   FECHA_INICIAL=	P_FEC_INICIAL_AU,
				   FECHA_FINAL=	P_FEC_FINAL_AU,
				   NUMERO_DIAS_TRABAJADOS=dias_faltados,
				   MONTO=	sueldo,
				   ID_NOMINA=	P_ID_NOMINA,
				   SEPTIMO =	P_SEPTIMO
			WHERE AUSENCIAS_ID= P_ID_AUSENCIA/*COD_TRB = _COD_TRB AND
				FECI_AU = FECHA_ANTIGUA  AND
				COD_NOM = _COD_NOM*/;
			 set salida = 1;
  
   END IF;
         WHEN "E" THEN
         DELETE FROM AUSENCIAS WHERE AUSENCIAS_ID = P_ID_AUSENCIA;
     END CASE;    
       
    IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'AUSENCIA',code,NOW(),v_nombre_columna,v_valor_campo);
      SET SALIDA = -1;
	else
		set salida =1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_mdescuentos`(
  IN P_ID_MDESCUENTOS INT,
  IN P_ID_EMPLEADO INT,
   IN P_ID_DESCUENTO INT,
   IN P_DESCRIPCION_DESCUENTO VARCHAR(30),
   IN P_CANT_DESCUENTO DECIMAL(17,2),
   IN P_FECHA_DESCUENTO DATE,
   IN P_MON_DESCUENTO DECIMAL(17,2),
   IN P_ID_CUENTA INT,
   #IN P_ID_NOMINA INT,
   IN P_ACCION VARCHAR(1),
   IN  P_ID_TIPO_PAGO INT,
   OUT p_salida int
)
BEGIN
	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_ID_EMPLEADO INT;
    
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    CASE P_ACCION
		 WHEN "N" THEN
			INSERT INTO MDESCUENTOS (
                 ID_EMPLEADO,
                 ID_DESCUENTO,
				 DESCRIPCION_DESCUENTO,
				 CANT_DESCUENTO,
				 FECHA_DESCUENTO,
				 MON_DESCUENTO,
				 ID_CUENTA,
				 ID_TIPO_PAGO)
			VALUES(P_ID_EMPLEADO,
                 P_ID_DESCUENTO,
				 P_DESCRIPCION_DESCUENTO,
				 P_CANT_DESCUENTO,
				 P_FECHA_DESCUENTO,
				 P_MON_DESCUENTO,
				 P_ID_CUENTA,
				 P_ID_TIPO_PAGO);
		  
      WHEN "M" THEN
		UPDATE MDESCUENTOS
		SET 
          DESCRIPCION_DESCUENTO= P_DESCRIPCION_DESCUENTO,
		  CANT_DESCUENTO=P_CANT_DESCUENTO,
		  FECHA_DESCUENTO=P_FECHA_DESCUENTO,
		  MON_DESCUENTO=P_MON_DESCUENTO,
		  ID_TIPO_PAGO=P_ID_TIPO_PAGO
		 WHERE ID_MDESCUENTOS = P_ID_MDESCUENTOS;
		
	WHEN "E" THEN
          DELETE FROM MDESCUENTOS
		  WHERE  ID_MDESCUENTOS = P_ID_MDESCUENTOS;
		
   END CASE; 
   
    IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'MOVIMIENTO DESCUENTO',code,NOW(),v_nombre_columna,v_valor_campo);
      SET P_SALIDA = -1;
	else
		set P_SALIDA =1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_mlabores`(
   IN P_ID_M_LABORES  INT,
   IN P_ID_EMPLEADO INT,
   IN P_ID_LABOR INT,
   IN P_DESC_LAB VARCHAR(30),
   IN P_TIPO_LAB VARCHAR(1),
   IN P_CANT_LAB DOUBLE(7,2),
   IN P_FEC_LAB DATETIME,
   IN P_MON_LAB DOUBLE(17,2),
   IN P_ID_CUENTA INT,
   #IN P_ID_NOMINA INT,
   IN P_ISR VARCHAR(1),
   IN P_ACCION VARCHAR(1),
   OUT p_salida int
)
BEGIN
     DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_ID_EMPLEADO INT;
    DECLARE P_FEC_ANTIGUA DATE;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    CASE P_ACCION
		 WHEN "N" THEN
			INSERT INTO mlabores (ID_EMPLEADO,
                 ID_LABOR,
				 DESCRIPCION_LAB,
				 ID_TIPO_PAGO,
				 CANTIDAD_LAB,
				 FECHA_LABOR,
				 MONTO_LABOR,
				 ID_CUENTA,
				 ISR)
			VALUES(P_ID_EMPLEADO,P_ID_LABOR,
				 P_DESC_LAB,P_TIPO_LAB,P_CANT_LAB,
				 P_FEC_LAB,P_MON_LAB,P_ID_CUENTA,
				 P_ISR);
		  
      WHEN "M" THEN
      SELECT FECHA_LABOR INTO P_FEC_ANTIGUA 
      FROM MLABORES  
      WHERE ID_MLABORES = P_ID_M_LABORES;
      
		UPDATE mlabores 
		SET 
        DESCRIPCION_LAB= P_DESC_LAB,
		 #TIPO_LAB= P_CANT_LAB,
		 #CANTIDAD_LAB=P_TIPO_LAB, 
		 FECHA_LABOR=P_FEC_LAB,
		 MONTO_LABOR=P_MON_LAB,
	     DESCRIPCION_LAB = P_DESC_LAB ,
	     CANTIDAD_LAB=P_CANT_LAB ,
	     FECHA_LABOR = P_FEC_LAB , 
	     MONTO_LABOR = P_MON_LAB ,
		 ID_CUENTA = P_ID_CUENTA, 
		 #ID_NOMINA = P_ID_NOMINA,
         ID_TIPO_PAGO = P_TIPO_LAB,
		 ISR=P_ISR
		 WHERE 
         ID_MLABORES = P_ID_M_LABORES;
         #ID_EMPLEADO = P_ID_EMPLEADO 
          #AND ID_LABOR = P_ID_LABOR
		   # AND FECHA_LABOR = P_FEC_ANTIGUA;
		
	WHEN "E" THEN
          DELETE FROM mlabores
		  WHERE ID_EMPLEADO=P_ID_EMPLEADO 
			&& ID_LABOR =P_ID_LABOR
			&& FECHA_LABOR = P_FEC_LAB; 
   END CASE; 
   
    IF code <> '00000' THEN  
	  INSERT INTO error_log (MENSAJE,TABLA,CODIGO_ERROR,FECHA_ERROR,NOMBRE_COLUMNA,VALOR_CAMPO) 
      values( MSG,'MOVIMIENTO LABORES',code,NOW(),v_nombre_columna,v_valor_campo);
      SET P_SALIDA = -1;
	else
		set P_SALIDA =1;
    END IF;
END ;



CREATE  PROCEDURE `acciones_mprestamo`(
  IN P_ACCION VARCHAR(1),
  IN P_ID_PRESTAMO INT,
  IN P_CODIGO VARCHAR(5),
  IN P_ID_EMPLEADO INT,
  IN P_FECHA DATE, 
  IN P_DESCRIPCION varchar(40),
  IN P_MONTO DECIMAL(17,2),
  IN P_ESTADO VARCHAR(1),
  IN P_CUOTA_MES DECIMAL(17,2),
  IN P_ID_TIPO_PAGO INT,
  IN P_TIEMPO INT,
  OUT P_SALIDA INT
)
BEGIN
	DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
    DECLARE V_DEBITO DECIMAL(17,2);
	DECLARE V_ACTUAL DECIMAL(17,2);
	DECLARE V_CREDITO DECIMAL(17,2);
	DECLARE V_ANTERIOR DECIMAL(17,2);
    DECLARE V_EXISTE INT DEFAULT 0;

	DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
		GET DIAGNOSTICS CONDITION 1
		code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT, v_nombre_columna = column_name;
    END;
    
    SET V_ANTERIOR = 0;
    SET V_CREDITO = 0;
    SET P_SALIDA = 0; -- Inicializamos la salida por seguridad
    
    CASE P_ACCION
		 WHEN "N" THEN
              SET V_DEBITO = P_MONTO;
			  SET V_ACTUAL = V_ANTERIOR + V_DEBITO - V_CREDITO;
              
              -- Evaluamos la función. Al ser nuevo, P_ID_PRESTAMO vendrá en 0
              IF existe_prestamo(P_ID_PRESTAMO, P_ID_EMPLEADO, P_FECHA) = 0 THEN
                  -- CORREGIDO: Se agregó la columna FECHA y su valor P_FECHA
				  INSERT INTO PRESTAMO(CODIGO, ID_EMPLEADO, FECHA, DESCRIPCION, MONTO, ESTADO, CUOTA_MES, ID_TIPO_PAGO, P_DEB, P_ACT, TIEMPO)
				  VALUES(P_CODIGO, P_ID_EMPLEADO, P_FECHA, P_DESCRIPCION, P_MONTO, 'A', P_CUOTA_MES, P_ID_TIPO_PAGO, P_MONTO, V_ACTUAL, P_TIEMPO);
                  
                  SET P_SALIDA = 1; -- Éxito
			  ELSE
                  SET code = '45000'; -- Forzamos código de error personalizado para detener el flujo exitoso
                  SET MSG = 'El empleado ya tiene un préstamo registrado en la fecha seleccionada.';
                  SET P_SALIDA = -2;  -- Código -2: Ya existe un préstamo en esa fecha
			  END IF;
			
		 WHEN "M" THEN
			  SET V_DEBITO = P_MONTO;
			  SET V_ACTUAL = V_ANTERIOR + V_DEBITO - V_CREDITO;
			
			  IF existe_prestamo(P_ID_PRESTAMO, P_ID_EMPLEADO, P_FECHA) = 0 THEN
				  UPDATE PRESTAMO
				  SET CODIGO = P_CODIGO,
					  FECHA = P_FECHA,
					  DESCRIPCION = P_DESCRIPCION,
					  MONTO = P_MONTO,
					  ESTADO = P_ESTADO,
					  CUOTA_MES = P_CUOTA_MES,
					  ID_TIPO_PAGO = P_ID_TIPO_PAGO,
					  TIEMPO = P_TIEMPO,
					  P_DEB = P_MONTO,
					  P_ACT = V_ACTUAL
				  WHERE ID_PRESTAMO = P_ID_PRESTAMO;
                  
				  SET P_SALIDA = 1; -- Éxito
			  ELSE
                  SET code = '45000';
                  SET MSG = 'No se puede modificar. El nuevo día elegido genera duplicidad de préstamos.';
				  SET P_SALIDA = -2; -- Código -2: Conflicto de fechas con otro préstamo
			  END IF;
              
		 WHEN "E" THEN
              -- Nota: Corregí el nombre de la columna a ID_PRESTAMO de acuerdo a tu UPDATE
			  DELETE FROM PRESTAMO WHERE ID_PRESTAMO = P_ID_PRESTAMO;
              SET P_SALIDA = 1; -- Éxito
    END CASE; 
    
    -- CONTROL FINAL DE SALIDAS Y ERRORES
    IF code <> '00000' THEN  
		INSERT INTO error_log (MENSAJE, TABLA, CODIGO_ERROR, FECHA_ERROR, NOMBRE_COLUMNA, VALOR_CAMPO) 
		VALUES (MSG, 'PRESTAMO', code, NOW(), v_nombre_columna, v_valor_campo);
        
        -- Si no fue un error controlado de duplicidad (-2), entonces fue un error severo de SQL (-1)
        IF P_SALIDA <> -2 THEN
			SET P_SALIDA = -1;
        END IF;
	END IF;
END ;



CREATE  PROCEDURE `acciones_parametro`(

	 IN P_ACCION VARCHAR(1),

     IN P_ID_PARAMETRO INT,

	 IN P_PERIODO INT,

	 IN P_EXCENTO DECIMAL(17,2),

     IN P_RANGO_INICIAL15 DECIMAL(17,2),

     IN P_RANGO_FINAL15 DECIMAL(17,2),

     IN P_RANGO_INICIAL20 DECIMAL(17,2),

     IN P_RANGO_FINAL20 DECIMAL(17,2),

     IN P_RANGO_INICIAL25 DECIMAL(17,2),

      IN P_SUELDO_PROMEDIO DECIMAL(17,2),

	  IN P_USUARIO VARCHAR(100),

      OUT P_SALIDA int

)
BEGIN

 SET @USUARIO = P_USUARIO;

   SET P_SALIDA =0;

CASE P_ACCION

		 WHEN "N" THEN

	       INSERT INTO parametro (PERIODO,EXCENTO , RANGO_INICIAL15 , RANGO_FINAL15,

                   RANGO_INICIAL20 , RANGO_FINAL20 , RANGO_INICIAL25,SUELDO_PROMEDIO )

                   VALUES(P_PERIODO , P_EXCENTO , P_RANGO_INICIAL15,

                   P_RANGO_FINAL15 ,

                   P_RANGO_INICIAL20 , P_RANGO_FINAL20 , P_RANGO_INICIAL25 ,

                   P_SUELDO_PROMEDIO);

                   SET P_SALIDA = 1;

    

	  

      WHEN "M" THEN

           UPDATE parametro SET

			   PERIODO = P_PERIODO , 

			   EXCENTO = P_EXCENTO ,

			   RANGO_INICIAL15 = P_RANGO_INICIAL15 , 

			   RANGO_FINAL15  = P_RANGO_FINAL15,

			   RANGO_INICIAL20 = P_RANGO_INICIAL20 ,

			   RANGO_FINAL20= P_RANGO_FINAL20 , 

			   RANGO_INICIAL25 = P_RANGO_INICIAL25 , 

			 

			   SUELDO_PROMEDIO = P_SUELDO_PROMEDIO

      WHERE

      PARAMETRO_ID = P_ID_PARAMETRO;

        

              SET P_SALIDA = 1;

           

	WHEN "E" THEN

          DELETE FROM PARAMETRO WHERE PARAMETRO_ID = P_ID_PARAMETRO;

            SET P_SALIDA = 1;

   END CASE; 

   

    

END ;



CREATE  PROCEDURE `buscar_antecedente`(
  IN  P_VALOR VARCHAR(30),
  IN P_CAMPO VARCHAR(7)
  )
BEGIN
  
  

      set @sql = concat( "SELECT *
						 FROM ANTECEDENTES
                         ", 
                        " WHERE ", P_CAMPO ," LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%')),
                        " OR ",P_CAMPO, " LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%' ))
                        );
            
    prepare stmt from @sql;

    execute stmt;
    DEALLOCATE PREPARE STMT;

END ;



CREATE  PROCEDURE `buscar_antecedentes`(
  IN  P_FECHA_VENCIMIENTO_INICIAL DATE,
  IN P_FECHA_VENCIMIENTO_FINAL DATE,
  IN P_TIPO_ANTECEDENTE VARCHAR(2)
  
  )
BEGIN
  
  

       set @sql = concat( "SELECT *
						 FROM ANTECEDENTES ", 
                        " WHERE ",
                        " FECHA_VENCIMIENTO BETWEEN ",QUOTE(P_FECHA_VENCIMIENTO_INICIAL),"
                          AND " ,QUOTE(P_FECHA_VENCIMIENTO_FINAL),
						" AND ",
						" TIPO_ANTECEDENTE=",QUOTE(P_TIPO_ANTECEDENTE)
                          
                        );
    prepare stmt from @sql;

    execute stmt;
    DEALLOCATE PREPARE STMT;

END ;



CREATE  PROCEDURE `buscar_aumentos_en_historial`(
  IN P_ID_EMPLEADO VARCHAR(5),
  IN P_FECI DATETIME,
  IN P_FECF DATETIME
)
BEGIN
     IF(P_FECF <> '1/1/0001') THEN
      SELECT 
		e.COD_TRB,
        e.NOM_TRB,
		FECHA,
		SUELDO_ANTERIOR,
		MONTO,
		SUELDO_ACTUAL,
        c.NOM_CAT
    FROM  historial_aumento AS a JOIN empleado as e
		 ON a.ID_EMPLEADO = e.ID_TRB  
     JOIN categoria AS c
		ON a.ID_CAT = c.ID_CAT
    WHERE e.ID_TRB = P_ID_EMPLEADO
    AND (FECHA BETWEEN  P_FECI AND P_FECF
              OR FECHA BETWEEN  P_FECI AND P_FECF);
        
     ELSE
		SELECT 
			e.COD_TRB,
			e.NOM_TRB,
			FECHA,
			SUELDO_ANTERIOR,
			MONTO,
			SUELDO_ACTUAL,
			c.NOM_CAT
		FROM  historial_aumento  AS a JOIN empleado as e
			 ON a.ID_EMPLEADO = e.ID_TRB   
		 JOIN categoria AS c
			ON a.ID_CAT = c.ID_CAT
		WHERE e.ID_TRB = P_ID_EMPLEADO 
		      AND FECHA = P_FECI ;
  END IF;
END ;



CREATE  PROCEDURE `buscar_categoria`(
  IN  P_VALOR VARCHAR(30),
  IN P_CAMPO VARCHAR(7)
  )
BEGIN
  
  

      set @sql = concat( "SELECT ID_CAT,COD_CAT, NOM_CAT,SAL_INI,SAL_FIN
						 FROM CATEGORIA
                         ", 
                        " WHERE ", P_CAMPO ," LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%')),
                        " OR ",P_CAMPO, " LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%' ))
                        );
            
    prepare stmt from @sql;

    execute stmt;
    DEALLOCATE PREPARE STMT;

END ;



CREATE  PROCEDURE `buscar_departamento`(
  IN  P_VALOR VARCHAR(30),
  IN P_CAMPO VARCHAR(7)
  )
BEGIN
  
  

      set @sql = concat( "SELECT D.ID_DEP,COD_DEP,NOM_DEP,e.COD_CUE,E.ID_TRB, E.NOM_TRB
						FROM departamento AS D
						LEFT JOIN EMPLEADO AS E 
						ON D.ID_EMPLEADO=E.ID_TRB",
                        " WHERE ", P_CAMPO ," LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%')),
                        " OR ",P_CAMPO, " LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%' ))
                        
                        );
            
    prepare stmt from @sql;

    execute stmt;
    DEALLOCATE PREPARE STMT;

END ;



CREATE  PROCEDURE `buscar_descuento`(
	 IN  P_VALOR VARCHAR(30),
     IN P_CAMPO VARCHAR(7)
)
BEGIN
		 set @sql = concat("SELECT ID_DESCUENTO,
					 COD_DEC,
					 NOM_DEC,
					 VAL_DEC,
					 FAC_DEC,
					 TP.ID_TIPO_PAGO,
                     TP.DESCRIPCION,
					 TJ.ID_TIPO_JORNADA,
                     TJ.DESCRIPCION,
					 ID_COD_CUE ,
					 TJ.DESCRIPCION,
					 TP.DESCRIPCION
					FROM descuento
                    AS D
					 INNER JOIN TIPO_JORNADA  AS TJ ON D.ID_TIPO_JORNADA = TJ.ID_TIPO_JORNADA   
					 INNER JOIN TIPO_PAGO AS TP ON D.ID_TIPO_PAGO = TP.ID_TIPO_PAGO  ",
					" WHERE ", P_CAMPO ," LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%')),
					" OR ",P_CAMPO, " LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%' ))
                        );
            
    prepare stmt from @sql;

    execute stmt;
    DEALLOCATE PREPARE STMT;
END ;



CREATE  PROCEDURE `buscar_empleado`(
   	 IN P_VALOR VARCHAR(30),
     IN P_CAMPO VARCHAR(7)
)
BEGIN

	 set @sql = concat("SELECT ID_TRB, 
	  e.COD_TRB, 
	  NOM_TRB, 
	  FEC_NAC, 
	  IDEN_TRB , 
	  EST_TRB,
      PASAPORTE, 
      RTN,
      ANTECEDENTES,
      IHS, 
      DIRECCION,
      TELEFONO, 
      FEC_DEF , 
      SEXO , 
      TE.ID_TIPO_EMPLEADO,
      TE.DESCRIPCION,
      PUESTO_TRABAJO, 
      SUELDO, 
      AFECTA_IHS, 
      AFECTA_FSV,
      AFECTA_SIN,
      AFECTA_ISR,
      e.ID_TIPO_PAGO, 
      BANCOS,
      NCUENTA,
      d.ID_DEP,
      d.NOM_DEP,
      c.ID_CAT,
      c.NOM_CAT,
      CELULAR,
      RESIDENCIA ,
      LICENCIA V,c.COD_CAT, c.SAL_INI, c.SAL_FIN,
      TPE.DESCRIPCION
	  FROM empleado as e
		INNER JOIN categoria AS c 
			ON e.ID_CAT = c.ID_CAT
		INNER JOIN departamento as d
			ON e.ID_DEP = d.ID_DEP
		INNER JOIN TIPO_EMPLEADO AS TE
		   ON  E.ID_TIPO_EMPLEADO = TE.ID_TIPO_EMPLEADO
		INNER JOIN TIPO_PAGO_EMPLEADO AS TPE
			ON E.ID_TIPO_PAGO = TPE.ID_TIPO_PAGO_EMPLEADO",
					" WHERE ", P_CAMPO ," LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%')),
					" OR ",P_CAMPO, " LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%' ))
                        );
            
    prepare stmt from @sql;
    execute stmt;
    DEALLOCATE PREPARE STMT;

END ;



CREATE  PROCEDURE `buscar_labor`(
	 IN  P_VALOR VARCHAR(30),
     IN P_CAMPO VARCHAR(7)
)
BEGIN
	 set @sql = concat( "SELECT ID_LAB,
		 COD_LAB,
		 NOM_LAB,
		 VAL_LAB,
		 FAC_LAB,	 
		 TP.ID_TIPO_PAGO,
		 TP.DESCRIPCION,
		 TJ.ID_TIPO_JORNADA,
		 TJ.DESCRIPCION,
		 ID_CUENTA
		 FROM labores   AS L
		  INNER JOIN TIPO_JORNADA  AS TJ ON L.ID_TIPO_JORNADA = TJ.ID_TIPO_JORNADA   
		  INNER JOIN TIPO_PAGO AS TP ON L.ID_TIPO_PAGO = TP.ID_TIPO_PAGO WHERE ",
					P_CAMPO ," LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%')),
					" OR ",P_CAMPO, " LIKE ", QUOTE( CONCAT( '%',P_VALOR, '%' ))
                        );
            
    prepare stmt from @sql;

    execute stmt;
    DEALLOCATE PREPARE STMT;

END ;



CREATE  PROCEDURE `buscar_maumentos`(
	IN P_ID_EMPLEADO INT,
    IN P_FECHA_INICIAL DATE,
    IN P_FECHA_FINAL DATE
)
BEGIN
 IF( P_FECHA_FINAL = '1/1/0001') THEN
	SELECT
		AUMENTOS_ID,
        ID_EMPLEADO,
        FECHA,
        ID_CATEGORIA,
        C.NOM_CAT,
        SUELDO_ANTERIOR,
        SUELDO_ACTUAL,
        TP.TIPO_AUMENTO_ID,
        TP.DESCRIPCION,
        PORCENTAJE,
        MONTO,
        TOTAL_MONTO,
        A.DESCRIPCION,
        E.NOM_TRB,  
        E.COD_TRB
    FROM 
        AUMENTOS AS A 
        INNER JOIN EMPLEADO AS E
           ON A.ID_EMPLEADO=E.ID_TRB
        INNER JOIN CATEGORIA AS C
           ON A.ID_CATEGORIA = C.ID_CAT
	    INNER JOIN TIPO_AUMENTO AS TP
           ON TP.TIPO_AUMENTO_ID = A.TIPO_AUMENTO_ID
   WHERE ID_EMPLEADO = P_ID_EMPLEADO AND FECHA = P_FECHA_INICIAL;
   ELSE 
     SELECT
		AUMENTOS_ID,
        ID_EMPLEADO,
        FECHA,
        ID_CATEGORIA,
        C.NOM_CAT,
        SUELDO_ANTERIOR,
        SUELDO_ACTUAL,
        TP.TIPO_AUMENTO_ID,
        TP.DESCRIPCION,
        PORCENTAJE,
        MONTO,
        TOTAL_MONTO,
        A.DESCRIPCION,
        E.NOM_TRB,
        E.COD_TRB
    FROM 
        AUMENTOS AS A 
        INNER JOIN EMPLEADO AS E
           ON A.ID_EMPLEADO=E.ID_TRB
        INNER JOIN CATEGORIA AS C
           ON A.ID_CATEGORIA = C.ID_CAT
	    INNER JOIN TIPO_AUMENTO AS TP
           ON TP.TIPO_AUMENTO_ID = A.TIPO_AUMENTO_ID
      WHERE ID_EMPLEADO = P_ID_EMPLEADO
           AND FECHA BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL;
      END IF;
   end ;



CREATE  PROCEDURE `buscar_mausencias`(
  IN P_COD_TRB VARCHAR(5),
  IN P_FECHA_INICIAL DATE,
  IN P_FECHA_FINAL DATE
)
BEGIN

 IF( P_FECHA_INICIAL <> '1/1/0001') THEN
    SELECT
   AUSENCIAS_ID,
	 E.COD_TRB,
	  E.NOM_TRB,
      
      ID_EMPLEADO,
      FECHA_INICIAL,
      FECHA_FINAL,
      NUMERO_DIAS_TRABAJADOS,
      MONTO,
      TP.DESCRIPCION_LARGA,
      SEPTIMO,
      TP.ID_TIPO_AUSENCIA
   FROM
      AUSENCIAS AS A
   INNER JOIN TIPO_AUSENCIA AS TP
     ON A.ID_TIPO_AUSENCIA = TP.ID_TIPO_AUSENCIA
   INNER JOIN  EMPLEADO AS E
     ON  E.ID_TRB=A.ID_EMPLEADO
   WHERE e.COD_TRB =P_COD_TRB AND (FECHA_INICIAL BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL
    OR FECHA_FINAL BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL)  ;
 ELSE    
      SELECT
   AUSENCIAS_ID,
	 E.COD_TRB,
	  E.NOM_TRB,
      
      ID_EMPLEADO,
      FECHA_INICIAL,
      FECHA_FINAL,
      NUMERO_DIAS_TRABAJADOS,
      MONTO,
      TP.DESCRIPCION_LARGA,
      SEPTIMO,
      TP.ID_TIPO_AUSENCIA
   FROM
      AUSENCIAS AS A
   INNER JOIN TIPO_AUSENCIA AS TP
     ON A.ID_TIPO_AUSENCIA = TP.ID_TIPO_AUSENCIA
   INNER JOIN  EMPLEADO AS E
     ON  E.ID_TRB=A.ID_EMPLEADO
      WHERE e.COD_TRB =P_COD_TRB AND (FECHA_INICIAL BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL
    OR FECHA_FINAL BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL)  ;
  END IF;
END ;



CREATE  PROCEDURE `buscar_mdescuentos`(
  IN P_COD_EMPLEADO VARCHAR(14),
  IN P_FEC_DEC_INICIAL DATE,
  IN P_FEC_DEC_FINAL DATE

)
BEGIN
  IF(p_FEC_DEC_INICIAL = '1/1/0001') THEN
	SELECT  
               
			 e.ID_trb,
              E.COD_TRB,
			 E.NOM_TRB ,
			 ID_DESCUENTO,
			 DESCRIPCION_DESCUENTO,
		     CANT_DESCUENTO,
			 FECHA_DESCUENTO,
			 MON_DESCUENTO,
			 md.ID_CUENTA,
			 TP.ID_TIPO_PAGO,
			 TP.DESCRIPCION,
             ID_MdESCUENTOS
             #D.NOM_DEP
		FROM MDESCUENTOS AS MD
		  INNER JOIN TIPO_PAGO AS TP
			ON MD.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
		  INNER JOIN EMPLEADO AS E
			ON E.ID_TRB  = MD.ID_EMPLEADO
         #    INNER JOIN DEPARTAMENTO AS D
        #ON D.ID_EMPLEADO = MD.ID_EMPLEADO
    WHERE 
		e.COD_TRB=P_COD_EMPLEADO AND FECHA_DESCUENTO=  P_FEC_DEC_FINAL;
        
    ELSEIF(P_FEC_DEC_FINAL = '1/1/0001') THEN
		      
		select	 e.ID_trb,
              E.COD_TRB,
			 E.NOM_TRB ,
			 ID_DESCUENTO,
			 DESCRIPCION_DESCUENTO,
		     CANT_DESCUENTO,
			 FECHA_DESCUENTO,
			 MON_DESCUENTO,
			 md.ID_CUENTA,
			 TP.ID_TIPO_PAGO,
			 TP.DESCRIPCION,
             ID_MdESCUENTOS
            # D.NOM_DEP
		FROM MDESCUENTOS AS MD
		  INNER JOIN TIPO_PAGO AS TP
			ON MD.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
		  INNER JOIN EMPLEADO AS E
			ON E.ID_TRB  = MD.ID_EMPLEADO
            # INNER JOIN DEPARTAMENTO AS D
             # ON D.ID_EMPLEADO = MD.ID_EMPLEADO
    WHERE 
		e.COD_TRB=P_COD_EMPLEADO AND FECHA_DESCUENTO=  P_FEC_DEC_INICIAL;
     ELSE 
     SELECT  
            
			 e.ID_trb,
              E.COD_TRB,
			 E.NOM_TRB ,
			 ID_DESCUENTO,
			 DESCRIPCION_DESCUENTO,
		     CANT_DESCUENTO,
			 FECHA_DESCUENTO,
			 MON_DESCUENTO,
			 md.ID_CUENTA,
			 TP.ID_TIPO_PAGO,
			 TP.DESCRIPCION,
             ID_MdESCUENTOS
            # D.NOM_DEP
		FROM MDESCUENTOS AS MD
		  INNER JOIN TIPO_PAGO AS TP
			ON MD.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
		  INNER JOIN EMPLEADO AS E
			ON E.ID_TRB  = MD.ID_EMPLEADO
		  #INNER JOIN DEPARTAMENTO AS D
           #    ON D.ID_EMPLEADO = MD.ID_EMPLEADO
          WHERE e.COD_TRB=P_COD_EMPLEADO
        AND FECHA_DESCUENTO BETWEEN P_FEC_DEC_INICIAL AND P_FEC_DEC_FINAL
        ;
  END IF;
END ;



CREATE  PROCEDURE `buscar_mlabores`(
  IN P_COD_EMPLEADO VARCHAR(5),
  IN P_FEC_LAB_INICIAL DATETIME,
  IN P_FEC_LAB_FINAL DATETIME
)
BEGIN
  IF(p_FEC_LAB_INICIAL = '1/1/0001') THEN
     SELECT  
      ID_EMPLEADO,
		 ID_LABOR,
		 DESCRIPCION_LAB,
		 TP.DESCRIPCION,
		 CANTIDAD_LAB,
		 FECHA_LABOR,
		 MONTO_LABOR,
		 ID_CUENTA,
		 ID_NOMINA,
         TP.ID_TIPO_PAGO,
         E.COD_TRB,
         E.NOM_TRB
         id_mlabores
    FROM mlabores as Ml JOIN empleado as e
       ON Ml.Ml.ID_EMPLEADO  = e.ID_TRB
      INNER JOIN TIPO_PAGO AS TP
		ON ML.ID_TIPO_PAGO = TP.ID_TIPO_PAGO 
    WHERE e.COD_TRB=P_COD_EMPLEADO AND FECHA_LABOR=  P_FEC_LAB_FINAL;
 
 
 ELSEIF(P_FEC_LAB_FINAL = '1/1/0001') THEN
     SELECT  
     ID_EMPLEADO,
		 ID_LABOR,
		 DESCRIPCION_LAB,
		 TP.DESCRIPCION,
		 CANTIDAD_LAB,
		 FECHA_LABOR,
		 MONTO_LABOR,
		 ID_CUENTA,
		 ID_NOMINA,
         TP.ID_TIPO_PAGO,
         E.COD_TRB,
         E.NOM_TRB,
		 id_mlabores
    FROM mlabores as Ml JOIN empleado as e
      ON Ml.ID_EMPLEADO  = e.ID_TRB
      INNER JOIN TIPO_PAGO AS TP
		ON ML.ID_TIPO_PAGO = TP.ID_TIPO_PAGO 
     WHERE e.COD_TRB=P_COD_EMPLEADO
      AND FECHA_LABOR = P_FEC_LAB_INICIAL;
      
 ELSE 
     SELECT  
       ID_EMPLEADO,
		 ID_LABOR,
		 DESCRIPCION_LAB,
		 TP.DESCRIPCION,
		 CANTIDAD_LAB,
		 FECHA_LABOR,
		 MONTO_LABOR,
		 ID_CUENTA,
		 ID_NOMINA,
         TP.ID_TIPO_PAGO,
         E.COD_TRB,
         E.NOM_TRB,
		 id_mlabores
    FROM mlabores as Ml JOIN empleado as e
       ON Ml.ID_EMPLEADO  = e.ID_TRB
      INNER JOIN TIPO_PAGO AS TP
		ON ML.ID_TIPO_PAGO = TP.ID_TIPO_PAGO 
          WHERE e.COD_TRB=P_COD_EMPLEADO
        AND FECHA_LABOR BETWEEN P_FEC_LAB_INICIAL AND P_FEC_LAB_FINAL
        ;
END IF;
END ;



CREATE  PROCEDURE `buscar_mprestamo`(
  IN P_ID_EMPLEADO INT,
    IN P_FECHA_INICIAL DATE,
    IN P_FECHA_FINAL DATE

)
BEGIN
  IF( P_FECHA_FINAL = '1/1/0001') THEN
	  SELECT
      P.ID_PRESTAMO,
      E.COD_TRB,
	  P.CODIGO,
      P.FECHA,
	  P.DESCRIPCION,
	  MONTO,
      CUOTA_MES,
      TIEMPO,
	  P.ESTADO,
	  P.ID_TIPO_PAGO,
      TP.DESCRIPCION,
	  P_ACT,
      E.NOM_TRB
  FROM PRESTAMO AS P
    INNER JOIN EMPLEADO AS E
           ON P.ID_EMPLEADO=E.ID_TRB
       INNER JOIN TIPO_PAGO_PRESTAMO AS TP
           ON P.ID_TIPO_PAGO = TP.ID_TIPO_PAGO_PRESTAMO
      WHERE p.ID_EMPLEADO = P_ID_EMPLEADO AND p.FECHA = P_FECHA_INICIAL;
         
  ELSE
      SELECT
       P.ID_PRESTAMO,
      E.COD_TRB,
	  P.CODIGO,
      P.FECHA,
	  P.DESCRIPCION,
	  MONTO,
      CUOTA_MES,
      TIEMPO,
	  P.ESTADO,
	  P.ID_TIPO_PAGO,
      TP.DESCRIPCION,
	  P_ACT,
      E.NOM_TRB
    FROM PRESTAMO AS P
    INNER JOIN EMPLEADO AS E
           ON P.ID_EMPLEADO=E.ID_TRB
       INNER JOIN TIPO_PAGO_PRESTAMO AS TP
           ON P.ID_TIPO_PAGO = TP.ID_TIPO_PAGO_PRESTAMO
      WHERE ID_EMPLEADO = P_ID_EMPLEADO
           AND FECHA BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL;
  END IF;
END ;



CREATE  PROCEDURE `buscar_mprestamos`(
  IN P_ID_EMPLEADO INT,
    IN P_FECHA_INICIAL DATE,
    IN P_FECHA_FINAL DATE

)
BEGIN
  IF( P_FECHA_FINAL = '1/1/0001') THEN
	  SELECT
      P.ID_PRESTAMO,
      E.COD_TRB,
	  P.CODIGO,
      P.FECHA,
	  P.DESCRIPCION,
	  MONTO,
      CUOTA_MES,
      TIEMPO,
	  P.ESTADO,
	  P.ID_TIPO_PAGO,
      TP.DESCRIPCION,
	  P_ACT,
      E.NOM_TRB,
      D.NOM_DEP
  FROM PRESTAMO AS P
       INNER JOIN EMPLEADO AS E
           ON P.ID_EMPLEADO=E.ID_TRB
       INNER JOIN TIPO_PAGO_PRESTAMO AS TP
           ON TP.ID_TIPO_PAGO_PRESTAMO = P.ID_TIPO_PAGO
	   INNER JOIN DEPARTAMENTO AS D
         ON D.ID_DEP= E.ID_DEP
      WHERE E.ID_TRB = P_ID_EMPLEADO AND FECHA = P_FECHA_INICIAL;
  ELSE
      SELECT
       P.ID_PRESTAMO,
      E.COD_TRB,
	  P.CODIGO,
      P.FECHA,
	  P.DESCRIPCION,
	  MONTO,
      CUOTA_MES,
      TIEMPO,
	  P.ESTADO,
	  P.ID_TIPO_PAGO,
      TP.DESCRIPCION,
	  P_ACT,
      E.NOM_TRB,
	D.NOM_DEP
    FROM PRESTAMO AS P
       INNER JOIN EMPLEADO AS E
           ON P.ID_EMPLEADO=E.ID_TRB
       INNER JOIN TIPO_PAGO_PRESTAMO AS TP
           ON TP.ID_TIPO_PAGO_PRESTAMO = P.ID_TIPO_PAGO
	   INNER JOIN DEPARTAMENTO AS D
      ON D.ID_DEP = E.ID_DEP
      WHERE E.ID_TRB = P_ID_EMPLEADO
           AND FECHA BETWEEN P_FECHA_INICIAL AND P_FECHA_FINAL;
  END IF;
END ;



CREATE  PROCEDURE `buscar_parametro`(

  IN P_PERIODO INT

)
BEGIN

   SELECT  PARAMETRO_ID ,

	  PERIODO ,

	 EXCENTO,

	 RANGO_INICIAL15,

     RANGO_FINAL15 ,

     RANGO_INICIAL20,

     RANGO_FINAL20 ,

     RANGO_INICIAL25,

      SUELDO_PROMEDIO

     FROM parametro

     WHERE PERIODO = P_PERIODO;

END ;

CREATE  PROCEDURE `eliminar_mlabores`(
	IN P_ID_MLABORES INT,
    IN P_ID_LABOR INT,
    IN P_FEC_LAB INT
)
BEGIN
   DELETE FROM MLABORES 
   WHERE 
     ID_MLABORES=P_ID_MLABORES
     AND ID_LABOR = P_ID_LABOR
     AND FECHA_LABOR = P_FEC_LAB;
END ;

CREATE  PROCEDURE `llenar_tabla_historial_sueldo`(
  IN P_ID_EMPLEADO INT,
  IN P_FECHA DATE
)
BEGIN
	DECLARE i int;
	DECLARE V_NUMERO_EMPLEADOS INT;
	DECLARE V_ANIO_VARCHAR VARCHAR(4);
    DECLARE V_MES VARCHAR(2);
    DECLARE V_COD_TRB VARCHAR(5);
    DECLARE V_SUELDO DECIMAL(17,2);
    DECLARE V_ANIO INT;
    DECLARE V_FECHA_EMPLEADO DATETIME;
    DECLARE V_FECHA_INGRESO_EMPLEADO DATETIME;
    DECLARE V_EXISTE_HISTORIAL INT;
    DECLARE V_TOTAL DECIMAL(17,2);
    
    select SUBSTRING_INDEX(P_FECHA, "-", 1) INTO V_ANIO_VARCHAR; 
    select SUBSTRING(P_FECHA, 6, 2)  INTO V_MES; 
	SELECT CAST(V_ANIO_VARCHAR AS  UNSIGNED) INTO V_ANIO ;
	
   SELECT COUNT(P_ID_EMPLEADO) INTO V_NUMERO_EMPLEADOS FROM EMPLEADO;
   SET i = 0;
   
  /* WHILE i < V_NUMERO_EMPLEADOS DO
    
		SELECT P_ID_EMPLEADO INTO V_COD_TRB 
        FROM EMPLEADO LIMIT i,1;*/
        
        #SET V_FECHA_EMPLEADO = SUBSTRING(P_FECHA, 1, 8); 
        
        /*SELECT FEC_DEF INTO V_FECHA_INGRESO_EMPLEADO 
        FROM EMPLEADO 
        WHERE COD_TRB = V_COD_TRB AND
        FEC_DEF BETWEEN CONCAT(V_FECHA_EMPLEADO,'01') AND CONCAT(V_FECHA_EMPLEADO,'30'); */
        
        #SET V_FECHA_INGRESO_EMPLEADO =SUBSTRING(V_FECHA_INGRESO_EMPLEADO, 1, 8); 
        
        SELECT COUNT(P_ID_EMPLEADO) INTO V_EXISTE_HISTORIAL
		FROM HISTORIAL_SUELDO
		WHERE COD_TRB = V_COD_TRB 
              AND PERIODO = V_ANIO;
              
        SET V_SUELDO = f_obtener_sueldo(V_COD_TRB); 
        
        IF (V_MES = '01' OR V_MES = '1')  THEN
             IF V_EXISTE_HISTORIAL = 1 THEN
				UPDATE HISTORIAL_SUELDO SET
                    ENERO = V_SUELDO,
					FEBRERO = V_SUELDO,
					MARZO= V_SUELDO,
					ABRIL= V_SUELDO,
					MAYO= V_SUELDO,
					JUNIO= V_SUELDO,
					JULIO= V_SUELDO,
					AGOSTO= V_SUELDO,
					SEPTIEMBRE= V_SUELDO,
					OCTUBRE= V_SUELDO,
					NOVIEMBRE= V_SUELDO,
					DICIEMBRE= V_SUELDO
                WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                ELSE
                 INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,ENERO,FEBRERO,MARZO,ABRIL,
							MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
							V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF;
          
          ELSEIF (V_MES = '02' OR V_MES = '2') THEN
             IF V_EXISTE_HISTORIAL = 1 THEN
             	UPDATE HISTORIAL_SUELDO SET
					FEBRERO = V_SUELDO,
					MARZO= V_SUELDO,
					ABRIL= V_SUELDO,
					MAYO= V_SUELDO,
					JUNIO= V_SUELDO,
					JULIO= V_SUELDO,
					AGOSTO= V_SUELDO,
					SEPTIEMBRE= V_SUELDO,
					OCTUBRE= V_SUELDO,
					NOVIEMBRE= V_SUELDO,
					DICIEMBRE= V_SUELDO
                WHERE   ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                ELSE
                 INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,FEBRERO,MARZO,ABRIL,
							MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(V_COD_TRB,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
							V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF;
              
             ELSEIF (V_MES = '03' OR V_MES = '3')  THEN 
				  IF V_EXISTE_HISTORIAL = 1 THEN
					UPDATE HISTORIAL_SUELDO SET
						MARZO= V_SUELDO,
						ABRIL= V_SUELDO,
						MAYO= V_SUELDO,
						JUNIO= V_SUELDO,
						JULIO= V_SUELDO,
						AGOSTO= V_SUELDO,
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE   ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
				      INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,MARZO,ABRIL,
					       MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
					   VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
					    V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF;
                
              ELSEIF (V_MES = '04' OR V_MES = '4' ) THEN 
				IF V_EXISTE_HISTORIAL = 1 THEN
					UPDATE HISTORIAL_SUELDO SET
						ABRIL= V_SUELDO,
						MAYO= V_SUELDO,
						JUNIO= V_SUELDO,
						JULIO= V_SUELDO,
						AGOSTO= V_SUELDO,
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO  AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,ABRIL,
							MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
							V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF;
                
			  ELSEIF ( V_MES = '05' OR V_MES = '5') THEN 
				IF V_EXISTE_HISTORIAL = 1 THEN
					UPDATE HISTORIAL_SUELDO SET
						MAYO= V_SUELDO,
						JUNIO= V_SUELDO,
						JULIO= V_SUELDO,
						AGOSTO= V_SUELDO,
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,
							MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES( P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
							V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF; 
                
                ELSEIF (V_MES = '06' OR V_MES = '6' ) THEN 
				   IF V_EXISTE_HISTORIAL = 1 THEN
					   UPDATE HISTORIAL_SUELDO SET
						JUNIO= V_SUELDO,
						JULIO= V_SUELDO,
						AGOSTO= V_SUELDO,
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,MARZO,
                         JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
							V_SUELDO,V_SUELDO);
			  END IF; 
                 
		       
			   ELSEIF ( V_MES = '07' OR V_MES = '7')  THEN 
				 IF V_EXISTE_HISTORIAL = 1 THEN
					   UPDATE HISTORIAL_SUELDO SET
						JULIO= V_SUELDO,
						AGOSTO= V_SUELDO,
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,
                         JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO_TRB,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,
							V_SUELDO);
			  END IF; 
                 
              
                ELSEIF (V_MES = '08' OR V_MES = '8')  THEN 
				  IF V_EXISTE_HISTORIAL = 1 THEN
					   UPDATE HISTORIAL_SUELDO SET
						AGOSTO= V_SUELDO,
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,
                       AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF; 
                 
             
				ELSEIF (V_MES = '09' OR V_MES = '9') THEN 
				  IF V_EXISTE_HISTORIAL = 1 THEN
					   UPDATE HISTORIAL_SUELDO SET
						SEPTIEMBRE= V_SUELDO,
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,
                       SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF; 
                
                  
				ELSEIF (V_MES = '10') THEN
				IF V_EXISTE_HISTORIAL = 1 THEN
					UPDATE HISTORIAL_SUELDO SET
						OCTUBRE= V_SUELDO,
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,
                         OCTUBRE,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO);
			  END IF; 
                
                ELSEIF (V_MES = '11')  THEN
				 IF V_EXISTE_HISTORIAL = 1 THEN
					UPDATE HISTORIAL_SUELDO SET
						NOVIEMBRE= V_SUELDO,
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,NOVIEMBRE,DICIEMBRE)
		           VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO);
			  END IF; 
                
                
                ELSEIF (V_MES = '12') THEN
				  IF V_EXISTE_HISTORIAL = 1 THEN
					UPDATE HISTORIAL_SUELDO SET
						DICIEMBRE= V_SUELDO
					WHERE  ID_TRB = P_ID_EMPLEADO AND PERIODO = V_ANIO;
                 ELSE
					INSERT INTO HISTORIAL_SUELDO ( ID_EMPLEADO ,PERIODO,DICIEMBRE)
		           VALUES( P_ID_EMPLEADO ,V_ANIO,V_SUELDO);
			  END IF; 
		       
        END IF;
      #SET i = i + 1;
     #END WHILE;
END ;


CREATE  PROCEDURE `llenar_tabla_historial_sueldo_empleado`(
  IN P_ID_EMPLEADO INT,
  IN P_FECHA DATE
)
BEGIN
    DECLARE V_ANIO_VARCHAR VARCHAR(4);
    DECLARE V_MES VARCHAR(2);
    DECLARE V_SUELDO DECIMAL(17,2);
    DECLARE V_ANIO INT;
    DECLARE V_EXISTE_HISTORIAL INT;
    
    SELECT SUBSTRING_INDEX(P_FECHA, "-", 1) INTO V_ANIO_VARCHAR; 
    SELECT SUBSTRING(P_FECHA, 6, 2)  INTO V_MES; 
    SELECT CAST(V_ANIO_VARCHAR AS UNSIGNED) INTO V_ANIO;
	
    SELECT COUNT(*) INTO V_EXISTE_HISTORIAL
    FROM HISTORIAL_SUELDO
    WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
              
    SET V_SUELDO = f_obtener_sueldo(P_ID_EMPLEADO); 
        
    IF (V_MES = '01' OR V_MES = '1') THEN
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                ENERO = V_SUELDO, FEBRERO = V_SUELDO, MARZO= V_SUELDO, ABRIL= V_SUELDO,
                MAYO= V_SUELDO, JUNIO= V_SUELDO, JULIO= V_SUELDO, AGOSTO= V_SUELDO,
                SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,ENERO,FEBRERO,MARZO,ABRIL,MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;
          
    ELSEIF (V_MES = '02' OR V_MES = '2') THEN
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                FEBRERO = V_SUELDO, MARZO= V_SUELDO, ABRIL= V_SUELDO, MAYO= V_SUELDO,
                JUNIO= V_SUELDO, JULIO= V_SUELDO, AGOSTO= V_SUELDO, SEPTIEMBRE= V_SUELDO,
                OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            -- CORREGIDO: Se cambió V_COD_TRB por P_ID_EMPLEADO
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,FEBRERO,MARZO,ABRIL,MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;
              
    ELSEIF (V_MES = '03' OR V_MES = '3') THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                MARZO= V_SUELDO, ABRIL= V_SUELDO, MAYO= V_SUELDO, JUNIO= V_SUELDO,
                JULIO= V_SUELDO, AGOSTO= V_SUELDO, SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO,
                NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,MARZO,ABRIL,MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;
                
    ELSEIF (V_MES = '04' OR V_MES = '4' ) THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                ABRIL= V_SUELDO, MAYO= V_SUELDO, JUNIO= V_SUELDO, JULIO= V_SUELDO,
                AGOSTO= V_SUELDO, SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO  AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,ABRIL,MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;
                
    ELSEIF ( V_MES = '05' OR V_MES = '5') THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                MAYO= V_SUELDO, JUNIO= V_SUELDO, JULIO= V_SUELDO, AGOSTO= V_SUELDO,
                SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,MAYO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF; 
                
    ELSEIF (V_MES = '06' OR V_MES = '6' ) THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                JUNIO= V_SUELDO, JULIO= V_SUELDO, AGOSTO= V_SUELDO, SEPTIEMBRE= V_SUELDO,
                OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            -- CORREGIDO: Se eliminó la columna errónea 'MARZO' de la lista de inserción del mes 6
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,JUNIO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;  
                 
    ELSEIF ( V_MES = '07' OR V_MES = '7') THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                JULIO= V_SUELDO, AGOSTO= V_SUELDO, SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO,
                NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            -- CORREGIDO: Se cambió P_ID_EMPLEADO_TRB por P_ID_EMPLEADO
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,JULIO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;  
              
    ELSEIF (V_MES = '08' OR V_MES = '8') THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                AGOSTO= V_SUELDO, SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,AGOSTO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;  
             
    ELSEIF (V_MES = '09' OR V_MES = '9') THEN 
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                SEPTIEMBRE= V_SUELDO, OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,SEPTIEMBRE,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;  
                
    ELSEIF (V_MES = '10') THEN
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                OCTUBRE= V_SUELDO, NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,OCTUBRE,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO,V_SUELDO);
        END IF;  
                
    ELSEIF (V_MES = '11') THEN
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                NOVIEMBRE= V_SUELDO, DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO,PERIODO,NOVIEMBRE,DICIEMBRE)
            VALUES(P_ID_EMPLEADO,V_ANIO,V_SUELDO,V_SUELDO);
        END IF;  
                
    ELSEIF (V_MES = '12') THEN
        IF V_EXISTE_HISTORIAL = 1 THEN
            UPDATE HISTORIAL_SUELDO SET
                DICIEMBRE= V_SUELDO
            WHERE ID_EMPLEADO = P_ID_EMPLEADO AND PERIODO = V_ANIO;
        ELSE
            INSERT INTO HISTORIAL_SUELDO (ID_EMPLEADO ,PERIODO,DICIEMBRE)
            VALUES(P_ID_EMPLEADO ,V_ANIO,V_SUELDO);
        END IF;       
    END IF;
END ;


CREATE  PROCEDURE `obtener_antecedente`(
IN P_ID INT,
IN P_ID_EMPLEADO INT,
IN P_TIPO_ANTECEDENTE VARCHAR(2)
)
BEGIN
SELECT ID_ANTECEDENTE,NUMERO_ANTECEDENTE, FECHA_EMISION,
		FECHA_VENCIMIENTO,VIGENCIA, LUGAR_ORIGEN , ID_EMPLEADO
	FROM ANTECEDENTES
    WHERE ID_ANTECEDENTE= P_ID
		AND ID_EMPLEADO = P_ID_EMPLEADO
		AND TIPO_ANTECEDENTE=P_TIPO_ANTECEDENTE;
END ;


CREATE  PROCEDURE `obtener_antecedentes`(
  IN P_TIPO_ANTECEDENTE VARCHAR(2),
  IN P_ID_EMPLEADO INT
  
)
BEGIN

	SELECT ID_ANTECEDENTE, NUMERO_ANTECEDENTE, FECHA_EMISION,
		FECHA_VENCIMIENTO,VIGENCIA, LUGAR_ORIGEN , ID_EMPLEADO
	FROM ANTECEDENTES
    WHERE TIPO_ANTECEDENTE=P_TIPO_ANTECEDENTE AND ID_EMPLEADO = P_ID_EMPLEADO
    ;


END ;


CREATE  PROCEDURE `obtener_categorias`()
BEGIN
  SELECT ID_CAT,COD_CAT,NOM_CAT,SAL_INI,SAL_FIN
  FROM categoria;
END ;


CREATE  PROCEDURE `obtener_departamentos`()
BEGIN
  SELECT  D.ID_DEP,D.COD_DEP,D.NOM_DEP,e.COD_CUE,E.ID_TRB, E.NOM_TRB
  FROM departamento AS D
  LEFT JOIN EMPLEADO AS E 
  ON D.ID_EMPLEADO=E.ID_TRB
  ORDER BY COD_DEP DESC ;
END ;



CREATE  PROCEDURE `obtener_descuento`(
	 IN P_ID_DEC INT
)
BEGIN
  SELECT ID_DESCUENTO,
     COD_DEC,
	 NOM_DEC,
     VAL_DEC,
	 FAC_DEC,
	 TJ.ID_TIPO_JORNADA,
     TJ.DESCRIPCION,
	 TP.ID_TIPO_PAGO,
     TP.DESCRIPCION,
	 ID_COD_CUE
 FROM descuento AS D
 INNER JOIN TIPO_JORNADA  AS TJ ON D.ID_TIPO_JORNADA = TJ.ID_TIPO_JORNADA   
 INNER JOIN TIPO_PAGO AS TP ON D.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
 WHERE ID_DESCUENTO= P_ID_DEC;
END ;



CREATE  PROCEDURE `obtener_descuentos`()
BEGIN
 SELECT ID_DESCUENTO,
     COD_DEC,
	 NOM_DEC,
     VAL_DEC,
	 FAC_DEC,
	 TJ.ID_TIPO_JORNADA,
     TJ.DESCRIPCION,
	 TP.ID_TIPO_PAGO,
     TP.DESCRIPCION,
	 ID_COD_CUE
 FROM descuento AS D
 INNER JOIN TIPO_JORNADA  AS TJ ON D.ID_TIPO_JORNADA = TJ.ID_TIPO_JORNADA   
 INNER JOIN TIPO_PAGO AS TP ON D.ID_TIPO_PAGO = TP.ID_TIPO_PAGO;
 
END ;



CREATE  PROCEDURE `obtener_descuento_por_codigo`(
	IN P_CODIGO VARCHAR(3)
)
BEGIN
 SELECT ID_DEC,
     COD_DEC,
	 NOM_DEC,
     VAL_DEC,
	 FAC_DEC,
	 T_JOR,
	 TIPO_DEC, 
	 COD_CUE
 FROM descuento
 WHERE COD_DEC  =P_CODIGO;
    
END ;



CREATE  PROCEDURE `obtener_empleado`( IN P_ID INT

)
BEGIN
 SELECT ID_TRB,
    e.COD_TRB, 
	  NOM_TRB, 
	  FEC_NAC, 
	  IDEN_TRB , 
	  EST_TRB,
      PASAPORTE, 
      RTN,
      ANTECEDENTES,
      IHS, 
      DIRECCION,
      TELEFONO, 
      FEC_DEF , 
      SEXO , 
      TE.ID_TIPO_EMPLEADO,
      TE.DESCRIPCION,
      PUESTO_TRABAJO, 
      SUELDO, 
      AFECTA_IHS, 
      AFECTA_FSV,
      AFECTA_SIN,
      AFECTA_ISR,
      e.ID_TIPO_PAGO, 
      BANCOS,
      NCUENTA,
      d.ID_DEP,
      d.NOM_DEP,
      c.ID_CAT,
      c.NOM_CAT,
      CELULAR,
      RESIDENCIA ,
      LICENCIA ,
     
     c.COD_CAT, 
      c.SAL_INI, 
      c.SAL_FIN,
      TPE.DESCRIPCION,
     
      e.FECHA_INICIO,
      E.TIPO_EMPLEADO,
	 
      CUENTA_SUELDO,
      CUENTA_SEGURO_SOCIAL,
      CUENTA_REGIMEN_ESPECIAL, 
      CUENTA_ISR,
      OTRA_CUENTA_1,
      OTRA_CUENTA_2
  FROM empleado as e
	INNER JOIN categoria AS c 
  ON e.ID_CAT = c.ID_CAT
	INNER JOIN departamento as d
  ON e.ID_DEP = d.ID_DEP
    INNER JOIN TIPO_EMPLEADO AS TE
      ON  E.ID_TIPO_EMPLEADO = TE.ID_TIPO_EMPLEADO
    INNER JOIN TIPO_PAGO_EMPLEADO AS TPE
    ON E.ID_TIPO_PAGO = TPE.ID_TIPO_PAGO_EMPLEADO
      WHERE ID_TRB = P_ID;
END ;

CREATE  PROCEDURE `obtener_empleados`()
BEGIN
  SELECT ID_TRB, 
	  e.COD_TRB, 
	  NOM_TRB, 
	  FEC_NAC, 
	  IDEN_TRB , 
	  EST_TRB,
      PASAPORTE, 
      RTN,
      ANTECEDENTES,
      IHS, 
      DIRECCION,
      TELEFONO, 
      FEC_DEF , 
      SEXO , 
      
      TE.ID_TIPO_EMPLEADO,
      TE.DESCRIPCION,
      
      PUESTO_TRABAJO, 
      SUELDO, 
      AFECTA_IHS, 
      AFECTA_FSV,
      AFECTA_SIN,
      AFECTA_ISR,
      ID_TIPO_PAGO, 
      BANCOS,
      NCUENTA,
      d.ID_DEP,
      d.NOM_DEP,
      c.ID_CAT,
      c.NOM_CAT,
      CELULAR,
      RESIDENCIA ,
      LICENCIA V,
      c.COD_CAT,
      c.SAL_INI,
      c.SAL_FIN,
      E.FECHA_INICIO
  FROM empleado as e
	INNER JOIN categoria AS c 
  ON e.ID_CAT = c.ID_CAT
	INNER JOIN departamento as d
  ON e.ID_DEP = d.ID_DEP
    INNER JOIN TIPO_EMPLEADO AS TE
      ON  E.ID_TIPO_EMPLEADO = TE.ID_TIPO_EMPLEADO; 
END ;

CREATE  PROCEDURE `obtener_empleado_codigo`( IN P_CODIGO varchar(10)

)
BEGIN
 SELECT ID_TRB, 
	  e.COD_TRB, 
	  NOM_TRB, 
	  FEC_NAC, 
	  IDEN_TRB , 
	  EST_TRB,
      PASAPORTE, 
      RTN,
      ANTECEDENTES,
      IHS, 
      DIRECCION,
      TELEFONO, 
      FEC_DEF , 
      SEXO , 
      #TIPO EMPLEADO
      TE.ID_TIPO_EMPLEADO,
      TE.DESCRIPCION,
      
      PUESTO_TRABAJO, 
      SUELDO, 
      AFECTA_IHS, 
      AFECTA_FSV,
      AFECTA_SIN,
      AFECTA_ISR,
      e.ID_TIPO_PAGO, 
      BANCOS,
      NCUENTA,
      d.ID_DEP,
      d.NOM_DEP,
      c.ID_CAT,
      c.NOM_CAT,
      CELULAR,
      RESIDENCIA ,
      LICENCIA V,
      c.COD_CAT, 
      c.SAL_INI, 
      c.SAL_FIN,
      TPE.DESCRIPCION,
      E.FECHA_INICIO,
      E.TIPO_EMPLEADO,
      C.SAL_INI,
      C.SAL_FIN
  FROM empleado as e
	INNER JOIN categoria AS c 
  ON e.ID_CAT = c.ID_CAT
	INNER JOIN departamento as d
  ON e.ID_DEP = d.ID_DEP
    INNER JOIN TIPO_EMPLEADO AS TE
      ON  E.ID_TIPO_EMPLEADO = TE.ID_TIPO_EMPLEADO
    INNER JOIN TIPO_PAGO_EMPLEADO AS TPE
    ON E.ID_TIPO_PAGO = TPE.ID_TIPO_PAGO_EMPLEADO
      WHERE COD_TRB = P_CODIGO;

END ;

CREATE  PROCEDURE `obtener_empleado_id`( IN P_ID INT

)
BEGIN
 SELECT ID_TRB, 
	  e.COD_TRB, 
	  NOM_TRB, 
	  FEC_NAC, 
	  IDEN_TRB , 
	  EST_TRB,
      PASAPORTE, 
      RTN,
      ANTECEDENTES,
      IHS, 
      DIRECCION,
      TELEFONO, 
      FEC_DEF , 
      SEXO , 
      #TIPO EMPLEADO
      TE.ID_TIPO_EMPLEADO,
      TE.DESCRIPCION,
      
      PUESTO_TRABAJO, 
      SUELDO, 
      AFECTA_IHS, 
      AFECTA_FSV,
      AFECTA_SIN,
      AFECTA_ISR,
      e.ID_TIPO_PAGO, 
      BANCOS,
      NCUENTA,
      d.ID_DEP,
      d.NOM_DEP,
      c.ID_CAT,
      c.NOM_CAT,
      CELULAR,
      RESIDENCIA ,
      LICENCIA V,
      c.COD_CAT, 
      c.SAL_INI, 
      c.SAL_FIN,
      TPE.DESCRIPCION,
      E.FECHA_INICIO,
      E.TIPO_EMPLEADO
  FROM empleado as e
	INNER JOIN categoria AS c 
  ON e.ID_CAT = c.ID_CAT
	INNER JOIN departamento as d
  ON e.ID_DEP = d.ID_DEP
    INNER JOIN TIPO_EMPLEADO AS TE
      ON  E.ID_TIPO_EMPLEADO = TE.ID_TIPO_EMPLEADO
    INNER JOIN TIPO_PAGO_EMPLEADO AS TPE
    ON E.ID_TIPO_PAGO = TPE.ID_TIPO_PAGO_EMPLEADO
      WHERE ID_TRB = P_ID;

END ;


CREATE  PROCEDURE `obtener_labor`(
  IN P_ID_LAB INT
)
BEGIN
 SELECT ID_LAB,
     COD_LAB,
	 NOM_LAB,
	 VAL_LAB,
	 FAC_LAB,
     TP.ID_TIPO_PAGO,
     TP.DESCRIPCION,
	 TJ.ID_TIPO_JORNADA,
     TJ.DESCRIPCION,
     ID_CUENTA
 FROM labores   AS L
  INNER JOIN TIPO_JORNADA  AS TJ ON L.ID_TIPO_JORNADA = TJ.ID_TIPO_JORNADA   
  INNER JOIN TIPO_PAGO AS TP ON L.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
 WHERE ID_LAB = P_ID_LAB;
END ;


CREATE  PROCEDURE `obtener_labores`()
BEGIN
 SELECT ID_LAB,
     COD_LAB,
	 NOM_LAB,
	 VAL_LAB,
	 FAC_LAB,	 
     TP.ID_TIPO_PAGO,
     TP.DESCRIPCION,
	 TJ.ID_TIPO_JORNADA,
     TJ.DESCRIPCION,
     ID_CUENTA
 FROM labores   AS L
  INNER JOIN TIPO_JORNADA  AS TJ ON L.ID_TIPO_JORNADA = TJ.ID_TIPO_JORNADA   
  INNER JOIN TIPO_PAGO AS TP ON L.ID_TIPO_PAGO = TP.ID_TIPO_PAGO;
END ;


CREATE  PROCEDURE `obtener_maumento`(
	IN P_AUMENTO_ID INT
)
BEGIN

	SELECT
		AUMENTOS_ID,
        ID_EMPLEADO,
        FECHA,
        ID_CATEGORIA,
        C.NOM_CAT,
        SUELDO_ANTERIOR,
        SUELDO_ACTUAL,
        TP.TIPO_AUMENTO_ID,
        TP.DESCRIPCION,
        PORCENTAJE,
        MONTO,
        TOTAL_MONTO,
        A.DESCRIPCION,
        E.NOM_TRB,
        E.COD_TRB
    FROM 
        AUMENTOS AS A 
        INNER JOIN EMPLEADO AS E
           ON A.ID_EMPLEADO=E.ID_TRB
        INNER JOIN CATEGORIA AS C
           ON A.ID_CATEGORIA = C.ID_CAT
	    INNER JOIN TIPO_AUMENTO AS TP
           ON TP.TIPO_AUMENTO_ID = A.TIPO_AUMENTO_ID
   WHERE AUMENTOS_ID = P_AUMENTO_ID;
END ;


CREATE  PROCEDURE `obtener_maumentos`()
BEGIN

	SELECT
		AUMENTOS_ID,
        ID_EMPLEADO,
        FECHA,
        ID_CATEGORIA,
        C.NOM_CAT,
        SUELDO_ANTERIOR,
        SUELDO_ACTUAL,
        TP.TIPO_AUMENTO_ID,
        TP.DESCRIPCION,
        PORCENTAJE,
        MONTO,
        TOTAL_MONTO,
        A.DESCRIPCION,
        E.NOM_TRB,
        E.COD_TRB
    FROM 
        AUMENTOS AS A 
        INNER JOIN EMPLEADO AS E
           ON A.ID_EMPLEADO=E.ID_TRB
        INNER JOIN CATEGORIA AS C
           ON A.ID_CATEGORIA = C.ID_CAT
	    INNER JOIN TIPO_AUMENTO AS TP
           ON TP.TIPO_AUMENTO_ID = A.TIPO_AUMENTO_ID;

END ;

CREATE  PROCEDURE `obtener_mausencia`(
  IN P_COD_TRB VARCHAR(5),
  IN P_FECHA_INICIAL DATE
)
BEGIN
   SELECT
   AUSENCIAS_ID,
	 E.COD_TRB,
	  E.NOM_TRB,
      
      ID_EMPLEADO,
      FECHA_INICIAL,
      FECHA_FINAL,
      NUMERO_DIAS_TRABAJADOS,
      MONTO,
      TP.DESCRIPCION_LARGA,
      SEPTIMO,
      TP.ID_TIPO_AUSENCIA
   FROM
      AUSENCIAS AS A
   INNER JOIN TIPO_AUSENCIA AS TP
     ON A.ID_TIPO_AUSENCIA = TP.ID_TIPO_AUSENCIA
   INNER JOIN  EMPLEADO AS E
     ON  E.ID_TRB=A.ID_EMPLEADO;
END ;

CREATE  PROCEDURE `obtener_mausencias`(
  
)
BEGIN
   SELECT
   AUSENCIAS_ID,
	 E.COD_TRB,
	  E.NOM_TRB,
      
      ID_EMPLEADO,
      FECHA_INICIAL,
      FECHA_FINAL,
      NUMERO_DIAS_TRABAJADOS,
      MONTO,
      TP.DESCRIPCION_LARGA,
      SEPTIMO,
      TP.ID_TIPO_AUSENCIA
   FROM
      AUSENCIAS AS A
   INNER JOIN TIPO_AUSENCIA AS TP
     ON A.ID_TIPO_AUSENCIA = TP.ID_TIPO_AUSENCIA
   INNER JOIN  EMPLEADO AS E
     ON  E.ID_TRB=A.ID_EMPLEADO;
END ;


CREATE  PROCEDURE `obtener_mdescuento`(IN P_ID INT)
BEGIN
	SELECT   
			 e.ID_trb,
              E.COD_TRB,
			 E.NOM_TRB ,
			 DE.ID_DESCUENTO,
			 DESCRIPCION_DESCUENTO,
		     CANT_DESCUENTO,
			 FECHA_DESCUENTO,
			 MON_DESCUENTO,
			 md.ID_CUENTA,
			 TP.ID_TIPO_PAGO,
			 TP.DESCRIPCION,
             ID_MdESCUENTOS,
             D.NOM_DEP,
             E.SUELDO,
             DE.cod_dec,
             MD.MON_DESCUENTO,
             DE.FAC_DEC,
             DE.VAL_DEC
		FROM MDESCUENTOS AS MD
		  INNER JOIN TIPO_PAGO AS TP
			ON MD.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
		  INNER JOIN EMPLEADO AS E
			ON E.ID_TRB  = MD.ID_EMPLEADO
		  INNER JOIN DEPARTAMENTO AS D
		   ON D.ID_DEP = E.ID_DEP
           INNER JOIN DESCUENTO AS DE
		   ON DE.ID_DESCUENTO = MD.ID_DESCUENTO
		WHERE ID_MDESCUENTOS = P_ID 
		;   
END ;


CREATE  PROCEDURE `obtener_mdescuentos`(

)
BEGIN
	SELECT  e.ID_trb,
			 E.COD_TRB,
			 E.NOM_TRB,
             ID_DESCUENTO,             
			 DESCRIPCION_DESCUENTO,
			 CANT_DESCUENTO,
			 FECHA_DESCUENTO,
			 MON_DESCUENTO,
			 md.ID_CUENTA,
     
			 TP.ID_TIPO_PAGO,
			 TP.DESCRIPCION,
             
             ID_MdESCUENTOS,
             D.NOM_DEP
    FROM MDESCUENTOS AS MD
      INNER JOIN TIPO_PAGO AS TP
		ON MD.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
      INNER JOIN EMPLEADO AS E
		ON E.ID_TRB  = MD.ID_EMPLEADO
      INNER JOIN DEPARTAMENTO AS D
        ON D.ID_EMPLEADO = MD.ID_EMPLEADO
    ;             
END ;



CREATE  PROCEDURE `obtener_mLabor`(
  IN P_ID_EMPLEADO VARCHAR(5),
  IN P_ID_LABOR VARCHAR(3),
  IN P_FEC_LAB DATE
)
BEGIN
  SELECT  
		 ml.ID_EMPLEADO,
		 ID_LABOR,
		 DESCRIPCION_LAB,
		 #TIPO_LAB,
		 CANTIDAD_LAB,
		 FECHA_LABOR,
		 MONTO_LABOR,
		 ml.ID_CUENTA,
		 ID_NOMINA,
         E.NOM_TRB,
         E.COD_TRB,
         tp.descripcion,
         ID_MLABORES
	  FROM mlabores ml INNER JOIN EMPLEADO E
	  ON ml.ID_EMPLEADO = e.id_trb
      INNER JOIN LABORES l ON ml.id_labor = l.id_lab
      inner join tipo_pago tp on tp.id_tipo_pago = l.id_tipo_pago
      WHERE ID_EMPLEADO = P_ID_EMPLEADO
		AND ID_LABOR = P_ID_LABOR 
		AND FECHA_LABOR = P_FEC_LAB;
END ;



CREATE  PROCEDURE `obtener_mLabores`()
BEGIN
SELECT  
		 ML.ID_EMPLEADO,
		 ID_LABOR,
		 DESCRIPCION_LAB,
		 TP.DESCRIPCION,
		 CANTIDAD_LAB,
		 FECHA_LABOR,
		 MONTO_LABOR,
		 ID_CUENTA,
		 ID_NOMINA,
         TP.ID_TIPO_PAGO,
         E.COD_TRB,
         E.NOM_TRB, 
         ID_MLABORES
	  FROM mlabores AS ML
      INNER JOIN TIPO_PAGO AS TP
		ON ML.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
      INNER JOIN EMPLEADO AS E
		ON E.ID_TRB  = ML.ID_EMPLEADO
      ;
END ;



CREATE  PROCEDURE `obtener_mprestamo`(
	IN P_ID_PRESTAMO INT
)
BEGIN
	
  SELECT
      P.ID_PRESTAMO,
      E.COD_TRB,
	  P.CODIGO,
      P.FECHA,
	  P.DESCRIPCION,
	  MONTO,
      CUOTA_MES,
      TIEMPO,
	  P.ESTADO,
	  P.ID_TIPO_PAGO,
      TP.DESCRIPCION,
	  P_ACT,
      E.NOM_TRB
  FROM PRESTAMO AS P
    INNER JOIN EMPLEADO AS E
           ON P.ID_EMPLEADO=E.ID_TRB
       INNER JOIN TIPO_PAGO_PRESTAMO AS TP
           ON TP.ID_TIPO_PAGO_PRESTAMO = P.ID_TIPO_PAGO
   WHERE ID_PRESTAMO = P_ID_PRESTAMO;
END ;



CREATE  PROCEDURE `obtener_mprestamos`(
	
)
BEGIN
	
  SELECT
      P.ID_PRESTAMO,
      E.COD_TRB,
	  P.CODIGO,
      P.FECHA,
	  P.DESCRIPCION,
	  MONTO,
      CUOTA_MES,
      TIEMPO,
	  P.ESTADO,
	  CUOTA,
	  P.ID_TIPO_PAGO,
      TP.DESCRIPCION,
	  P_ACT,
      
      E.NOM_TRB
  FROM PRESTAMO AS P
    INNER JOIN EMPLEADO AS E
           ON P.ID_EMPLEADO=E.ID_TRB
       INNER JOIN TIPO_PAGO_PRESTAMO AS TP
           ON TP.ID_TIPO_PAGO_PRESTAMO = P.ID_TIPO_PAGO;
END ;


