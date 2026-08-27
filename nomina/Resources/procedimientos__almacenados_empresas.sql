
use sistema_nomina;

CREATE DEFINER=`root`@`localhost` FUNCTION `existe_aumento`(
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

CREATE DEFINER=`root`@`localhost` FUNCTION `existe_departamento`(
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

CREATE DEFINER=`root`@`localhost` FUNCTION `existe_prestamo`(
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

CREATE DEFINER=`root`@`localhost` FUNCTION `existe_rango_fecha_ausencia`(
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
CREATE DEFINER=`root`@`localhost` FUNCTION `fn_calcular_ihss`(
    p_salario DECIMAL(10,2)
    , p_anio INT
) RETURNS decimal(10,2)
    DETERMINISTIC
BEGIN

    -- Variables
    DECLARE v_techo_ihss DECIMAL(12,2) DEFAULT 11290.180;

    DECLARE v_base_calculo DECIMAL(12,2);

    DECLARE v_ihss_empleado DECIMAL(12,2);
    DECLARE v_ihss_patrono DECIMAL(12,2);

    DECLARE v_total_ihss DECIMAL(12,2);

    -- ============================================
    -- BASE DE CÁLCULO
    -- Se usa el salario o el techo máximo
    -- ============================================
    SELECT p.VALOR_TECHO_IHSS INTO v_techo_ihss 
    FROM PARAMETRO P
    WHERE  p.periodo = p_anio;
    
    IF p_salario > v_techo_ihss THEN
        SET v_base_calculo = v_techo_ihss;
    ELSE
        SET v_base_calculo = p_salario;
    END IF;

    -- ============================================
    -- CÁLCULO IHSS
    -- ============================================

    -- Empleado 2.5%
    SET v_ihss_empleado = v_base_calculo * 0.0250;


    RETURN ROUND(v_ihss_empleado, 2);

END;

CREATE DEFINER=`root`@`localhost` FUNCTION `fn_calcular_rap`(
    p_salario DECIMAL(10,2),
    p_anio INT
) RETURNS decimal(10,2)
    DETERMINISTIC
BEGIN

    -- Variables
    DECLARE v_techo_reserva DECIMAL(12,2) DEFAULT 57896.160;

    DECLARE v_reserva_laboral DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_piso  DECIMAL(12,2) DEFAULT 0;
    DECLARE v_foviif DECIMAL(12,2) DEFAULT 0;
    DECLARE v_total_rap DECIMAL(12,2) DEFAULT 0;

    -- ============================================
    -- FONDO DE RESERVA LABORAL
    -- 4% patronal con techo de 3 salarios mínimos
    -- ============================================
    SELECT p.RESERVA_LAB_RAP INTO v_techo_reserva 
    FROM PARAMETRO P
    WHERE  p.periodo = p_anio;

      IF p_salario > v_techo_reserva THEN
        SET v_reserva_laboral = v_techo_reserva * 0.040;
    ELSE
        SET v_reserva_laboral = p_salario * 0.040;
    END IF;

  
 
    -- ============================================
    -- FOVIIF
    -- 1.5% empleado + 1.5% patrono
    -- SOLO sobre excedente de L 11,903.13
    -- ============================================
SELECT p.VALOR_PISO_RAP INTO v_piso 
    FROM PARAMETRO P
    WHERE  p.periodo = p_anio;
    
    IF p_salario > v_piso THEN
        SET v_foviif = (p_salario - v_piso) * 0.015;
    END IF;

    -- Total RAP
    SET v_total_rap = v_reserva_laboral + v_foviif;

    RETURN ROUND(v_total_rap,2);

END ;

CREATE DEFINER=`root`@`localhost` FUNCTION `fn_isr`(
    p_salario_mensual DECIMAL(12,2),
    p_anio INT,
    p_edad INT
) RETURNS decimal(12,2)
    DETERMINISTIC
BEGIN
    DECLARE v_renta_neta DECIMAL(12,2);
    DECLARE v_isr DECIMAL(12,2) DEFAULT 0.00;
	DECLARE v_valor_ipp DECIMAL(17,2); 
	DECLARE v_afecta_isr VARCHAR(1);
    -- Variables para los límites de los tramos
    DECLARE v_excento DECIMAL(12,2);
    DECLARE v_final15 DECIMAL(12,2);
    DECLARE v_final20 DECIMAL(12,2);

    DECLARE v_rap DECIMAL(12,2) default 0.00;
    DECLARE v_ihss DECIMAL(12,2) default 0.00;


    DECLARE v_ioa DECIMAL(12,2);          -- Ingresos Ordinarios Anuales
    DECLARE v_pa DECIMAL(12,2);           -- Prestaciones Adicionales (Aguinaldo + 14vo)
    DECLARE v_exento_pa DECIMAL(12,2);    -- Techo exento de prestaciones (10 salarios mínimos)
    DECLARE v_iag DECIMAL(12,2);          -- Ingreso Adicional Gravable
    DECLARE v_igt DECIMAL(12,2);          -- Ingreso Gravable Total
    DECLARE v_deducciones DECIMAL(12,2);  -- Deducciones por edad / gastos médicos
    DECLARE v_rng DECIMAL(12,2);          -- Renta Neta Gravable
    DECLARE v_salario_minimo_promedio DECIMAL(12,2);    -- salario minimo promedio
    
    
    
    -- 1. Cargar los parámetros reales de la tabla
    SELECT p.EXCENTO, p.RANGO_FINAL15, p.RANGO_FINAL20, p.salario_minimo_promedio, IFNULL(ipp, 0.00)
    INTO v_excento, v_final15, v_final20, v_salario_minimo_promedio,v_valor_ipp
    FROM PARAMETRO p
    WHERE p.periodo = p_anio;


    -- 1. Cálculos Iniciales de Ingresos
    SET v_ioa = p_salario_mensual * 12;
    SET v_pa = p_salario_mensual * 2;
    SET v_exento_pa = v_salario_minimo_promedio * 10;
    
    -- Aplicar regla de los 10 salarios mínimos (max(0, PA - Exento))
    IF v_pa > v_exento_pa THEN
        SET v_iag = v_pa - v_exento_pa;
    ELSE
        SET v_iag = 0;
    END IF;
    SET v_igt = v_ioa + v_iag;
    
    
    IF p_edad >= 60 AND v_igt <= 350000.00 THEN
        RETURN 0.00;
    END IF;
    
    -- ============================================================
    -- REGLA DE LOS 65 AÑOS: EXONERACIÓN CONDICIONADA
    -- Si tiene 65 o más y su renta bruta anual no supera L. 350,000, no paga nada.
    -- ============================================================
    IF p_edad >= 65  THEN
        SET v_deducciones = 80000.00;
    ELSE
        SET v_deducciones = 40000.00; -- Menores de 60 años
    END IF;

    -- 2. Convertir deducciones mensuales a proyección anual (Incluye Reserva Laboral + FOVIIF)
     SET v_rap = fn_calcular_rap(p_salario_mensual, p_anio) * 12;
    SET v_ihss = fn_calcular_ihss(p_salario_mensual, p_anio) * 12;

    -- 3. Deducción de Renta Neta Gravable
    #SET v_renta_neta = p_ingreso_anual - 40000.00 - v_rap - v_ihss;
	SET v_rng = v_igt - v_deducciones - v_rap - v_ihss - v_valor_ipp;
    
    -- Si no alcanza la base imponible exenta, el impuesto es 0
	IF v_rng <= v_excento THEN
        RETURN 0.00;
    END IF;
    -- 4. Cálculo exacto por acumulación de tramos progresivos
    IF v_rng <= v_final15 THEN
        -- Tramo 2: Cobra 15% sobre lo que excede de la base exenta
        SET v_isr = (v_rng - v_excento) * 0.15;
        
    ELSEIF v_rng <= v_final20 THEN
        -- Tramo 3: Tramo 2 completo + 20% sobre lo que excede al Tramo 2
        SET v_isr = ((v_final15 - v_excento) * 0.15) 
                  + ((v_rng - v_final15) * 0.20);
        
    ELSE
        -- Tramo 4: Tramo 2 completo + Tramo 3 completo + 25% sobre lo que excede al Tramo 3
        SET v_isr = ((v_final15 - v_excento) * 0.15) 
                  + ((v_final20 - v_final15) * 0.20) 
                  + ((v_rng - v_final20) * 0.25);
    END IF;

    RETURN ROUND(v_isr/12, 2);
END ;

CREATE DEFINER=`root`@`localhost` FUNCTION `fn_isr_adulto_mayor`(
    p_ingreso_anual DECIMAL(12,2),
    p_anio INT,
    p_edad INT
) RETURNS decimal(12,2)
    DETERMINISTIC
BEGIN
    DECLARE v_renta_neta DECIMAL(12,2);
    DECLARE v_isr DECIMAL(12,2) DEFAULT 0.00;
    DECLARE v_deduccion_edad DECIMAL(12,2) DEFAULT 0.00;

    -- Variables para los límites de los tramos
    DECLARE v_excento DECIMAL(12,2);
    DECLARE v_final15 DECIMAL(12,2);
    DECLARE v_final20 DECIMAL(12,2);

    DECLARE v_rap DECIMAL(12,2);
    DECLARE v_ihss DECIMAL(12,2);

    -- ============================================================
    -- REGLA DE LOS 65 AÑOS: EXONERACIÓN CONDICIONADA
    -- Si tiene 65 o más y su renta bruta anual no supera L. 350,000, no paga nada.
    -- ============================================================
    IF p_edad >= 65 AND p_ingreso_anual <= 350000.00 THEN
        RETURN 0.00;
    END IF;

    -- ============================================================
    -- DETERMINACIÓN DE DEDUCCIONES ADICIONALES POR EDAD
    -- Si tiene 60 o más (incluyendo los de 65+ que superaron los 350k 
    -- y por ende deben calcular ISR), se les otorga la deducción especial.
    -- ============================================================
    IF p_edad >= 60 THEN
        SET v_deduccion_edad = 80000.00;
    END IF;

    -- Cargar los parámetros de la tabla de control
    SELECT p.EXCENTO, p.RANGO_FINAL15, p.RANGO_FINAL20
    INTO v_excento, v_final15, v_final20
    FROM PARAMETRO p
    WHERE p.periodo = p_anio;

    -- Convertir deducciones mensuales a proyección anual (Incluye Reserva + FOVIIF)
    SET v_rap = fn_calcular_rap((p_ingreso_anual / 12), p_anio) * 12;
    SET v_ihss = fn_calcular_ihss((p_ingreso_anual / 12), p_anio) * 12;

    -- Deducción de Renta Neta Gravable
    -- Base = Ingreso - Gastos Médicos (40k) - Deducción Edad (30k si aplica) - RAP - IHSS
    SET v_renta_neta = p_ingreso_anual - 40000.00 - v_deduccion_edad - v_rap - v_ihss;

    -- Si la renta neta no supera el tramo exento (t1) o es negativa, queda en 0
    IF v_renta_neta <= v_excento OR v_renta_neta < 0 THEN
        RETURN 0.00;
    END IF;

    -- Cálculo acumulativo por tramos progresivos (SAR)
    IF v_renta_neta <= v_final15 THEN
        SET v_isr = (v_renta_neta - v_excento) * 0.150;
        
    ELSEIF v_renta_neta <= v_final20 THEN
        SET v_isr = ((v_final15 - v_excento) * 0.150) 
                  + ((v_renta_neta - v_final15) * 0.2000);
        
    ELSE
        SET v_isr = ((v_final15 - v_excento) * 0.15000) 
                  + ((v_final20 - v_final15) * 0.2000) 
                  + ((v_renta_neta - v_final20) * 0.25000);
    END IF;

    RETURN ROUND(v_isr, 2);
END ;
CREATE DEFINER=`root`@`localhost` FUNCTION `fn_numero_a_letras`(p_numero DECIMAL(12,2)) RETURNS varchar(500) CHARSET utf8mb4
    DETERMINISTIC
BEGIN
    DECLARE v_entero BIGINT;
    DECLARE v_decimales INT;
    DECLARE v_letras VARCHAR(500) DEFAULT '';
    DECLARE v_tramo INT;
    DECLARE v_contador INT DEFAULT 1;
    DECLARE v_texto_tramo VARCHAR(100);
    
    -- Variables para descomponer el tramo
    DECLARE u, d, c INT;

    SET v_entero = FLOOR(p_numero);
    SET v_decimales = ROUND((p_numero - v_entero) * 100);

    IF v_entero = 0 THEN
        SET v_letras = 'CERO';
    END IF;

    WHILE v_entero > 0 DO
        SET v_tramo = v_entero % 1000;
        SET v_texto_tramo = '';

        IF v_tramo > 0 THEN
            SET c = FLOOR(v_tramo / 100);
            SET d = FLOOR((v_tramo % 100) / 10);
            SET u = v_tramo % 10;

            -- Evaluar Centenas
            IF c = 1 AND (d > 0 OR u > 0) THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'CIENTO ');
            ELSEIF c = 1 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'CIEN ');
            ELSEIF c = 2 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'DOSCIENTOS ');
            ELSEIF c = 3 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'TRESCIENTOS ');
            ELSEIF c = 4 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'CUATROCIENTOS ');
            ELSEIF c = 5 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'QUINIENTOS ');
            ELSEIF c = 6 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'SEISCIENTOS ');
            ELSEIF c = 7 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'SETECIENTOS ');
            ELSEIF c = 8 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'OCHOCIENTOS ');
            ELSEIF c = 9 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'NOVECIENTOS ');
            END IF;

            -- Evaluar Decenas y Unidades
            IF d = 1 THEN
                IF u = 0 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'DIEZ ');
                ELSEIF u = 1 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'ONCE ');
                ELSEIF u = 2 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'DOCE ');
                ELSEIF u = 3 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'TRECE ');
                ELSEIF u = 4 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'CATORCE ');
                ELSEIF u = 5 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'QUINCE ');
                ELSE SET v_texto_tramo = CONCAT(v_texto_tramo, 'DIECI', ELT(u, 'UNO', 'DOS', 'TRES', 'CUATRO', 'CINCO', 'SEIS', 'SIETE', 'OCHO', 'NUEVE'), ' ');
                END IF;
            ELSEIF d = 2 THEN
                IF u = 0 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'VEINTE ');
                ELSE SET v_texto_tramo = CONCAT(v_texto_tramo, 'VEINTI', ELT(u, 'UNO', 'DOS', 'TRES', 'CUATRO', 'CINCO', 'SEIS', 'SIETE', 'OCHO', 'NUEVE'), ' ');
                END IF;
            ELSE
                IF d = 3 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'TREINTA');
                ELSEIF d = 4 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'CUARENTA');
                ELSEIF d = 5 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'CINCUENTA');
                ELSEIF d = 6 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'SESENTA');
                ELSEIF d = 7 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'SETENTA');
                ELSEIF d = 8 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'OCHENTA');
                ELSEIF d = 9 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, 'NOVENTA');
                END IF;
                
                IF d > 2 AND u > 0 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, ' Y '); END IF;
                IF u > 0 THEN SET v_texto_tramo = CONCAT(v_texto_tramo, ELT(u, 'UNO', 'DOS', 'TRES', 'CUATRO', 'CINCO', 'SEIS', 'SIETE', 'OCHO', 'NUEVE'), ' '); END IF;
            END IF;

            IF v_contador = 2 THEN 
                IF v_tramo = 1 THEN SET v_texto_tramo = 'MIL '; ELSE SET v_texto_tramo = CONCAT(v_texto_tramo, 'MIL '); END IF;
            ELSEIF v_contador = 3 THEN 
                IF v_tramo = 1 THEN SET v_texto_tramo = 'UN MILLON '; ELSE SET v_texto_tramo = CONCAT(v_texto_tramo, 'MILLONES '); END IF;
            END IF;

            SET v_letras = CONCAT(v_texto_tramo, v_letras);
        END IF;

        SET v_contador = v_contador + 1;
        SET v_entero = FLOOR(v_entero / 1000);
    END WHILE;

    -- MODIFICACIÓN AQUÍ: Evaluamos si tiene o no centavos
    IF v_decimales > 0 THEN
        RETURN TRIM(CONCAT(v_letras, ' CON ', LPAD(v_decimales, 2, '0'), '/100'));
    ELSE
        RETURN TRIM(v_letras); -- O puedes usar: RETURN CONCAT(TRIM(v_letras), ' EXACTOS');
    END IF;
END ;

CREATE DEFINER=`root`@`localhost` FUNCTION `f_obtener_sueldo`(
  V_ID_EMPLEADO INT
) RETURNS decimal(17,2)
    DETERMINISTIC
begin
      declare v_sueldo decimal(17,2);
    SELECT SUELDO INTO V_SUELDO FROM EMPLEADO WHERE ID_TRB = V_ID_EMPLEADO;
		  
RETURN v_sueldo;
END ;

CREATE DEFINER=`root`@`localhost` FUNCTION `obtener_sueldo_empleado_ausencia`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_antecedente`(
    IN P_ID_ANTECEDENTE  INT,
	 IN P_NUMERO_ANTECEDENTE INT(32),
	 IN P_FECHA_EMISION DATE,
	 IN  P_FECHA_VENCIMIENTO DATE,
	 IN P_VIGENCIA DATE,
	 IN  P_LUGAR_ORIGEN VARCHAR(100),
     IN P_TIPO_ANTECEDENTE VARCHAR(2),
	 IN P_ACCION VARCHAR(1),
     IN P_ID_EMPLEADO INT,
     IN P_USUARIO VARCHAR(500),
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
  DECLARE V_CODIGO_EMPLEADO VARCHAR(500);


/*DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;*/
    
    select cod_trb into v_codigo_empleado 
    from empleado
    where id_trb = p_id_empleado;
    
     SET @usuario_actual = P_USUARIO;
    CASE P_ACCION
		 WHEN "N" THEN

				 INSERT INTO ANTECEDENTES(NUMERO_ANTECEDENTE, FECHA_EMISION,
					FECHA_VENCIMIENTO,VIGENCIA, LUGAR_ORIGEN , ID_EMPLEADO,TIPO_ANTECEDENTE
				  )
			     VALUES(P_NUMERO_ANTECEDENTE, P_FECHA_EMISION, P_FECHA_VENCIMIENTO,P_VIGENCIA, P_LUGAR_ORIGEN , P_ID_EMPLEADO, P_TIPO_ANTECEDENTE);
				 CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'ANTECEDENTES', CONCAT('Se registro un antecedente para el empleado con código: ',  cod_trb));
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
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_categoria`(
   IN P_ACCION VARCHAR(1),
  IN P_ID_CATEGORIA INT,
  IN P_COD_CAT VARCHAR(3),
  IN P_NOM_CAT VARCHAR(30),
  IN P_SAL_INI DECIMAL(17,2),
  IN P_SAL_FIN DECIMAL(17,2),
  IN P_USUARIO VARCHAR(500),
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
    
    SET @usuario_actual = P_USUARIO;
    CASE P_ACCION
		 WHEN "N" THEN
				IF P_ID_CATEGORIA = -1 THEN
				  SET P_ID_CATEGORIA= NULL;
				END IF;
					INSERT INTO categoria (COD_CAT, NOM_CAT,SAL_INI,SAL_FIN)
					VALUES(P_COD_CAT,P_NOM_CAT,P_SAL_INI,P_SAL_FIN);
		CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'CATEGORIAS', CONCAT('Se registro una categoria para con el código: ', P_COD_CAT ));
   WHEN "M" THEN
		IF P_ID_CATEGORIA = -1 THEN
			SET P_ID_CATEGORIA= NULL;
		END IF;
		UPDATE categoria
			SET  
				COD_CAT =P_COD_CAT, 
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_departamento`(
   IN P_ACCION VARCHAR(1),
   IN P_COD_DEP VARCHAR(3),
   IN P_NOM_DEP VARCHAR(30),
   IN P_ID_EMPLEADO int,
   IN P_ID_CUENTA INT,
   IN P_ID_DEP INT,
   IN P_USUARIO VARCHAR(500),
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
     SET @usuario_actual = P_USUARIO;
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
		
        CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'DEPARTAMENTO', CONCAT('Se registro un departamento con código: ', P_COD_DEP ));
		 
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_descuento`(
   IN P_COD_DEC VARCHAR(3), 
   IN P_NOM_DEC VARCHAR(30), 
   IN P_VAL_DEC DECIMAL(17,2), 
   IN P_FAC_DEC DECIMAL(17,7), 
   IN P_ID_TIPO_JORNADA INT, 
   IN P_ID_TIPO_PAGO INT, 
   IN P_ID_COD_CUE VARCHAR(8),
   IN P_ACCION VARCHAR(1),
   IN P_ID_DEC INT,
   IN P_USUARIO VARCHAR(500),
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
    SET @usuario_actual = P_USUARIO;
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
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'DESCUENTOS', CONCAT('Se registro  descuento con código: ', P_COD_DEC ));
		  
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_empleado`(
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
     IN P_CUENTA_SUELDO VARCHAR(30),
     IN P_CUENTA_SEGURO_SOCIAL VARCHAR(30),
     IN P_CUENTA_REGIMEN_ESPECIAL VARCHAR(30),
     IN P_CUENTA_ISR VARCHAR(30),
     IN P_OTRA_CUENTA_1  VARCHAR(30),
     IN P_OTRA_CUENTA_2 VARCHAR(30),
     IN P_USUARIO VARCHAR(500),
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
     SET @usuario_actual = P_USUARIO;
    CASE P_ACCION
		 WHEN "N" THEN
			 INSERT INTO empleado (COD_TRB,NOM_TRB, FEC_NAC, IDEN_TRB , EST_TRB,
				PASAPORTE, RTN,ANTECEDENTES, IHS, DIRECCION,
				TELEFONO, FEC_DEF , SEXO , ID_TIPO_EMPLEADO, ID_DEP, ID_CAT,
				 PUESTO_TRABAJO, SUELDO, AFECTA_IHS, AFECTA_FSV,AFECTA_SIN, AFECTA_ISR,
                 ID_TIPO_PAGO,BANCOS,NCUENTA,
				CELULAR,RESIDENCIA,LICENCIA,FECHA_INICIO,TIPO_EMPLEADO,
                CUENTA_SUELDO,CUENTA_SEGURO_SOCIAL,CUENTA_REGIMEN_ESPECIAL,
                CUENTA_ISR,OTRA_CUENTA_1,OTRA_CUENTA_2, FECHA_CONTRATACION, ESTADO
              )
		   VALUES(P_COD_TRB,P_NOM_TRB, P_FEC_NAC, P_IDEN_TRB , P_EST_TRB,
					P_PAST_TRB, P_RTN_TRB,P_ANT_TRB, P_IHS_TRB, P_DIR_TRB,
					P_TEL_TRB, P_FEC_DEF , P_SEX_TRB , P_TIPO_TRB, P_ID_DEP, P_ID_CAT,
					P_PUEST_TRB, P_SUELDO, P_A_IHS, P_A_FSV,P_A_SIN, P_A_ISR,P_ID_FORMA_PAGO,
					P_BANCOS,P_NCUENTA,P_CELULAR_TRB,P_RESIDENCIA_TRB,
					P_LICENCIA_TRB,P_FECHA_INICIO,P_TIPO_EMPLEADO,P_CUENTA_SUELDO,
                    P_CUENTA_SEGURO_SOCIAL,P_CUENTA_REGIMEN_ESPECIAL,
                    P_CUENTA_ISR,P_OTRA_CUENTA_1,P_OTRA_CUENTA_2,P_FEC_DEF, 'A'
                    );
			
             IF code = '00000' THEN
				 SET V_ID_EMPLEADO = LAST_INSERT_ID();

				 CALL llenar_tabla_historial_sueldo_empleado(V_ID_EMPLEADO, P_FEC_DEF);
                               
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'EMPLEADO', CONCAT('Se registro un empleado con código: ', P_COD_TRB ));
		  
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_labor`(
	 IN P_ID_LAB INT,
     IN P_COD_LAB VARCHAR(3),
	 IN P_NOM_LAB VARCHAR(30),
	 IN P_TIPO_JORNADA VARCHAR(1),
	 IN P_VAL_LAB DOUBLE(17,2),
	 IN P_FAC_LAB DOUBLE(17,7),
	 IN P_ID_TIPO_PAGO INT, 
	 IN P_ID_CUENTA INT(8),
     IN P_ACCION VARCHAR(1),
     IN P_USUARIO VARCHAR(500),
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
     SET @usuario_actual = P_USUARIO;
    CASE P_ACCION
		 WHEN "N" THEN
            INSERT INTO labores(COD_LAB,NOM_LAB,ID_TIPO_JORNADA,
              VAL_LAB,FAC_LAB,ID_TIPO_PAGO,ID_CUENTA)
           VALUES (P_COD_LAB , P_NOM_LAB, P_TIPO_JORNADA,
                P_VAL_LAB, P_FAC_LAB, P_ID_TIPO_PAGO , P_ID_CUENTA);
                              
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'LABORES', CONCAT('Se registro una labor con código: ', P_COD_LAB ));
		  
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
 
CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_maumentos`(
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
 IN P_USUARIO VARCHAR(150),
 OUT P_SALIDA INT
)
BEGIN
   DECLARE MSG VARCHAR(100);
	DECLARE code CHAR(5) DEFAULT '00000';
	DECLARE v_nombre_columna text;
	DECLARE v_valor_campo text;
	DECLARE V_ID_EMPLEADO INT;
    DECLARE V_FECHA_ANTIGUA DATE;
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION

    BEGIN
      GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT,v_nombre_columna = column_name;
    END;
    
    SET @usuario_actual = P_USUARIO;
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
			    CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'AUMENTOS', CONCAT('Se registro un aumento para el empleado con el id: ',  P_ID_EMPLEADO));
	
				   SET P_SALIDA = 1;
	    ELSE 
			SET P_SALIDA = 0;
	    END IF;
         
         WHEN "M" THEN
             SELECT FECHA INTO V_FECHA_ANTIGUA
             FROM HISTORIAL_AUMENTO
             WHERE ID_EMPLEADO = P_ID_EMPLEADO
                   AND ID_CAT= P_ID_CATEGORIA
			ORDER BY FECHA DESC LIMIT 1;
            
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
                   AND FECHA = V_FECHA_ANTIGUA;
          SET P_SALIDA = 1;
      
	
        
         
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_mausencias`(
   IN P_ACCION VARCHAR(1),
   IN P_ID_AUSENCIA INT,
   IN P_ID_EMPLEADO INT,
   IN P_ID_TIPO_AUSENCIA INT,
   IN P_FEC_INICIAL_AU DATE,
   IN P_FEC_FINAL_AU DATE,
   IN P_ID_NOMINA VARCHAR(8),
   IN P_SEPTIMO VARCHAR(1),
   IN P_MONTO DECIMAL(17,2),
   IN P_USUARIO VARCHAR(200),
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
 
     SET @usuario_actual = P_USUARIO;
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
                         
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'AUSENCIAS', CONCAT('Se registro una ausencia para el empleado con id: ', P_ID_EMPLEADO ));
		  
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
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'AUSENCIAS', CONCAT('Se registro una ausencia para el empleado con id: ', P_ID_EMPLEADO ));
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_mdescuentos`(
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
   IN P_USUARIO VARCHAR(200),
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
     SET @usuario_actual = P_USUARIO;
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
		                
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'MDESCUENTOS', CONCAT('Se registro un movimiento de descuento para el empleado con id: ', P_ID_EMPLEADO ));
		  
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_mlabores`(
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
   IN P_USUARIO VARCHAR(500),
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
    SET @usuario_actual = P_USUARIO;
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
		              
			CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'MLABORES', CONCAT('Se registro un Movimiento descuento para el empleado con id: ', P_ID_EMPLEADO ));
		    
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_mprestamo`(
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
			  #SET V_ACTUAL = V_ANTERIOR + V_DEBITO - V_CREDITO;
              
              -- Evaluamos la función. Al ser nuevo, P_ID_PRESTAMO vendrá en 0
              #IF existe_prestamo(P_ID_PRESTAMO, P_ID_EMPLEADO, P_FECHA) = 0 THEN
                  -- CORREGIDO: Se agregó la columna FECHA y su valor P_FECHA
				  INSERT INTO PRESTAMO(CODIGO, ID_EMPLEADO, FECHA, DESCRIPCION, MONTO, ESTADO, CUOTA_MES, ID_TIPO_PAGO, P_DEB, P_ACT, TIEMPO)
				  VALUES(P_CODIGO, P_ID_EMPLEADO, P_FECHA, P_DESCRIPCION, P_MONTO, 'A', P_CUOTA_MES, P_ID_TIPO_PAGO, P_MONTO, P_MONTO, P_TIEMPO);
                  
                  SET P_SALIDA = 1; -- Éxito
			  #ELSE
               /*   SET code = '45000'; -- Forzamos código de error personalizado para detener el flujo exitoso
                  SET MSG = 'El empleado ya tiene un préstamo registrado en la fecha seleccionada.';
                  SET P_SALIDA = -2;  -- Código -2: Ya existe un préstamo en esa fecha*/
			  #END IF;
			
		 WHEN "M" THEN
			  SET V_DEBITO = P_MONTO;
			  SET V_ACTUAL = V_ANTERIOR + V_DEBITO - V_CREDITO;
			
			  #IF existe_prestamo(P_ID_PRESTAMO, P_ID_EMPLEADO, P_FECHA) = 0 THEN
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
					  P_ACT = P_MONTO
				  WHERE ID_PRESTAMO = P_ID_PRESTAMO;
                  
				  SET P_SALIDA = 1; -- Éxito
			  #ELSE
               /*   SET code = '45000';
                  SET MSG = 'No se puede modificar. El nuevo día elegido genera duplicidad de préstamos.';
				  SET P_SALIDA = -2; -- Código -2: Conflicto de fechas con otro préstamo
			  END IF;***/
              
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `acciones_parametro`(

	 IN P_ACCION VARCHAR(1),
     IN P_ID_PARAMETRO INT,
	 IN P_PERIODO INT,
	 IN P_EXCENTO DECIMAL(17,2),
     IN P_RANGO_INICIAL15 DECIMAL(17,2),
     IN P_RANGO_FINAL15 DECIMAL(17,2),
     IN P_RANGO_INICIAL20 DECIMAL(17,2),
     IN P_RANGO_FINAL20 DECIMAL(17,2),
     IN P_RANGO_INICIAL25 DECIMAL(17,2),
     IN P_RANGO_FINAL25 DECIMAL(17,2),
	 IN P_SUELDO_PROMEDIO DECIMAL(17,2),
	 IN P_USUARIO VARCHAR(100),
     IN P_RESERVA_LABORAL_RAP DECIMAL(17,2),
     IN P_VALOR_PISO_RAP DECIMAL(17,2),
     IN P_SALARIO_MINIMO_PROMEDIO DECIMAL(17,2),
     IN P_VALOR_TECHO_IHSS DECIMAL(17,2),
     IN P_VALOR_SINDICATO DECIMAL(17,2),
     IN P_VALOR_IPP DECIMAL(17,2),
	 OUT P_SALIDA int
)
BEGIN

 SET @USUARIO = P_USUARIO;

   SET P_SALIDA =0;
   SET @usuario_actual = P_USUARIO;
   
	CASE P_ACCION

		 WHEN "N" THEN
	       INSERT INTO parametro (PERIODO,EXCENTO , RANGO_INICIAL15 , RANGO_FINAL15,
                   RANGO_INICIAL20 , RANGO_FINAL20 , RANGO_INICIAL25, RANGO_FINAL25,SUELDO_PROMEDIO, 
                   RESERVA_LAB_RAP,
                   VALOR_PISO_RAP, VALOR_TECHO_IHSS, SALARIO_MINIMO_PROMEDIO,
                   SINDICATO, IPP
                   )
                   VALUES(P_PERIODO , P_EXCENTO , P_RANGO_INICIAL15,
                   P_RANGO_FINAL15 ,
                   P_RANGO_INICIAL20 , 
                   P_RANGO_FINAL20 ,
                   P_RANGO_INICIAL25 ,
                   P_RANGO_FINAL25 ,
                   P_SUELDO_PROMEDIO,
                   P_RESERVA_LABORAL_RAP,
                   P_VALOR_PISO_RAP,
                   P_VALOR_TECHO_IHSS,
                   P_SALARIO_MINIMO_PROMEDIO,
                   P_VALOR_SINDICATO,
                   P_VALOR_IPP
                   );
                   CALL sp_registrar_bitacora(p_usuario, 'NUEVO', 'PARAMETRO', CONCAT('Se registro un parámetro para el periodo: ', P_PERIODO ));
		  
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
			   SUELDO_PROMEDIO = P_SUELDO_PROMEDIO,
               RESERVA_LAB_RAP = P_RESERVA_LABORAL_RAP,
               VALOR_PISO_RAP = P_VALOR_PISO_RAP,
               VALOR_TECHO_IHSS = P_VALOR_TECHO_IHSS,
               SALARIO_MINIMO_PROMEDIO = P_SALARIO_MINIMO_PROMEDIO,
               SINDICATO = P_VALOR_SINDICATO,
               IPP =P_VALOR_IPP
         WHERE PARAMETRO_ID = P_ID_PARAMETRO;
		 
         SET P_SALIDA = 1;

	WHEN "E" THEN
		DELETE FROM PARAMETRO WHERE PARAMETRO_ID = P_ID_PARAMETRO;
         SET P_SALIDA = 1;
   END CASE; 
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_antecedente`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_antecedentes`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_aumentos_en_historial`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_categoria`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_departamento`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_descuento`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_empleado`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_labor`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_maumentos`(
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
   
CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_mausencias`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_mdescuentos`(
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_mlabores`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_mprestamo`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_mprestamos`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `buscar_parametro`(
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
     RANGO_FINAL25,
	 SUELDO_PROMEDIO,
	 RESERVA_LAB_RAP,
	 VALOR_PISO_RAP, 
     VALOR_TECHO_IHSS, 
     SALARIO_MINIMO_PROMEDIO
     FROM parametro
     WHERE PERIODO = P_PERIODO;

END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `eliminar_mlabores`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `llenar_tabla_historial_sueldo`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `llenar_tabla_historial_sueldo_empleado`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_antecedente`(
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_antecedentes`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_categorias`()
BEGIN
  SELECT ID_CAT,COD_CAT,NOM_CAT,SAL_INI,SAL_FIN
  FROM categoria;
END ;
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_departamentos`()
BEGIN
  SELECT  D.ID_DEP,D.COD_DEP,D.NOM_DEP,e.COD_CUE,E.ID_TRB, E.NOM_TRB
  FROM departamento AS D
  LEFT JOIN EMPLEADO AS E 
  ON D.ID_EMPLEADO=E.ID_TRB
  ORDER BY COD_DEP DESC ;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_descuento`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_descuentos`()
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_descuento_por_codigo`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_empleado`( IN P_ID INT

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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_empleados`()
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_empleado_codigo`( IN P_CODIGO varchar(10)

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
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_empleado_id`( IN P_ID INT

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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_labor`(
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


CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_labores`()
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_maumento`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_maumentos`()
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mausencia`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mausencias`(
  
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mdescuento`(IN P_ID INT)
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mdescuentos`(

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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mLabor`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mLabores`()
BEGIN
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
         ID_MLABORES
	  FROM mlabores AS ML
      INNER JOIN TIPO_PAGO AS TP
		ON ML.ID_TIPO_PAGO = TP.ID_TIPO_PAGO
      INNER JOIN EMPLEADO AS E
		ON E.ID_TRB  = ML.ID_EMPLEADO
      ;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mprestamo`(
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_mprestamos`(
	
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtener_parametro`(
  IN P_PARAMETRO_ID INT
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
     RANGO_FINAL25,
	 SUELDO_PROMEDIO,
	 RESERVA_LAB_RAP,
	 VALOR_PISO_RAP, 
     VALOR_TECHO_IHSS, 
     SALARIO_MINIMO_PROMEDIO
     FROM parametro
     WHERE PARAMETRO_ID = P_PARAMETRO_ID;

END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_constancia_empleado`(
    IN p_Filtro INT
)
BEGIN


    SELECT 
        e.`NOM_TRB` as NOMBRE,
		e.`FECHA_INICIO` AS FECHA_ING,
		e.`PUESTO_TRABAJO` AS PUESTO_TRABAJO,
	    e.`SUELDO`,
        e.`IDEN_TRB` as IDENTIDAD,
        tp.descripcion as TIPO_EMPLEADO,
        fn_numero_a_letras(e.`SUELDO`) AS SUELDO_LETRAS
    FROM `empleado` e
    JOIN tipo_empleado TP on e.ID_TIPO_EMPLEADO = TP.ID_TIPO_EMPLEADO
    WHERE e.`ID_TRB` = p_Filtro;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_consultar_bitacora`(
    IN P_MODO VARCHAR(30), -- 'ULTIMO_ACCESO', 'DETALLE_ACCESOS' o 'ULTIMA_MODIFICACION'
    IN P_NOMBRE_USUARIO VARCHAR(250)
)
BEGIN
    
  
    -- =========================================================================
    -- CASO 2: Historial detallado de todos los accesos (LOGINS) del usuario
    -- =========================================================================
    IF p_modo = 'DETALLE_ACCESOS' THEN
        SELECT 
            `id_bitacora` AS ID,
            `nombre_usuario`,
            `accion`,
            `descripcion` AS DESCRIPCION_DETALLAADA,
            `fecha_registro` AS FECHA_HORA
        FROM `bitacora`
        WHERE`nombre_usuario` = P_NOMBRE_USUARIO 
          AND `accion` = 'LOGIN'
        ORDER BY `fecha_registro` DESC;

    -- =========================================================================
    -- CASO 3: Última modificación de datos realizada por el usuario (DML)
    -- =========================================================================
    ELSEIF p_modo = 'ULTIMA_MODIFICACION' THEN
        SELECT 
            `id_bitacora` AS ID,
            `nombre_usuario`,
            `accion`,
            `tabla_afectada`,
            REPLACE(`descripcion`, ': ,', ': ') AS DESCRIPCION_DETALLADA,
            `fecha_registro` AS FECHA_HORA
        FROM `bitacora`
        WHERE `nombre_usuario` = P_NOMBRE_USUARIO 
          AND `accion` IN ('NUEVO', 'MODIFICAR', 'DELETE')
        ORDER BY `fecha_registro` DESC
        LIMIT 1;
        
    END IF;

END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ficha_empleado`(
    IN p_id_empleado INT
)
BEGIN

    SELECT 
        e.`COD_TRB`, 
        e.`NOM_TRB`,
		(SELECT d.`NOM_DEP` FROM `departamento` d WHERE d.`ID_DEP` = e.`ID_DEP`) AS DEPTO,
        e.`DIRECCION`,
        e.`FEC_NAC` AS FECHA_NAC,
        e.`SEXO`,
        e.`RTN`,
        e.`FECHA_INICIO` AS FECHA_ING,
		e.`PUESTO_TRABAJO` AS PUESTO_TRABAJO,
        (SELECT d.`NOM_CAT` FROM `categoria` d WHERE d.`ID_CAT` = e.`ID_CAT`) AS CATEGORIA,
        e.`CELULAR` AS TELEFONO,
        e.`IDEN_TRB` AS IDENTIDAD,
        e.`EST_TRB` AS ESTADO_CIVIL,
        e.`PASAPORTE`,
        e.`IHS` AS CARNET_IHSS,
	    e.`SUELDO`
    FROM `empleado` e
    WHERE e.`ID_TRB` = p_id_empleado;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_generar_planilla`(
    IN p_cod_planilla VARCHAR(10),
    IN p_fecha DATE,
    IN p_anio INT,
    IN p_tipo_planilla VARCHAR(150),
    OUT P_SALIDA INT
)
main_procedure: BEGIN
    -- =========================================================================
    -- BLOQUE 1: DECLARACIÓN DE VARIABLES COMUNES
    -- =========================================================================
    DECLARE msg VARCHAR(100);
    DECLARE code CHAR(5) DEFAULT '00000';
    DECLARE v_nombre_columna TEXT;
    DECLARE v_valor_campo TEXT;
    DECLARE v_fin_cursor INT DEFAULT 0;
    
    -- Variables de datos del empleado
    DECLARE v_id_empleado INT;
    DECLARE v_sueldo_base DECIMAL(17,2);
    DECLARE v_fecha_nac DATE;
    DECLARE v_edad INT;
    
    -- Banderas de afectación de impuestos
    DECLARE v_afecta_ihs VARCHAR(1);
    DECLARE v_afecta_fsv VARCHAR(1); 
    DECLARE v_afecta_isr VARCHAR(1);
	DECLARE v_afecta_sindicato VARCHAR(1);
     
    -- Variables para cálculos y acumulados
    DECLARE v_diario DECIMAL(17,2);
    DECLARE v_total_labores DECIMAL(17,2);
    DECLARE v_total_ausencias DECIMAL(17,2);
    DECLARE v_cuota_prestamos DECIMAL(17,2);
    DECLARE v_total_descuentos DECIMAL(17,2);
    
    -- VARIABLES PARA CONTROL DE PRÉSTAMOS
    DECLARE v_id_prestamo INT;
    DECLARE v_p_deb DECIMAL(17,2);
    DECLARE v_p_cred DECIMAL(17,2);
    DECLARE v_saldo_actual DECIMAL(17,2);
    DECLARE v_cuota_mes DECIMAL(17,2);
    DECLARE v_id_tipo_pago INT;
    DECLARE v_fin_cursor_pres INT DEFAULT 0;
    DECLARE v_aplicar_prestamo INT;
    
    -- Retenciones de leyes sociales
    DECLARE v_ihss_calc DECIMAL(17,2);
    DECLARE v_rap_calc DECIMAL(17,2);
    DECLARE v_isr_calc DECIMAL(17,2);
    declare V_VALOR_SINDICATO decimal(17,2);
	declare V_VALOR_IPP decimal(17,2);
	DECLARE v_sindicato_empleado DECIMAL(17,2);
    
    -- Totales agrupados requeridos por la tabla planilla
    DECLARE v_total_deducciones DECIMAL(17,2);
    DECLARE v_salario_bruto DECIMAL(17,2);
    DECLARE v_salario_neto DECIMAL(17,2);
    
    
    DECLARE v_fecha_inicio DATE;
    DECLARE v_fecha_fin DATE;
    
    -- Variables de control de fechas
    DECLARE v_dia_pago INT;
    DECLARE v_es_quincena_1 INT DEFAULT 0;

	DECLARE V_ANTICIPO VARCHAR(150);
    DECLARE V_EXISTE_ANTICIPO INT;
  
    -- =========================================================================
    -- BLOQUE 2: DECLARACIÓN DE CURSORES
    -- =========================================================================
    DECLARE cur_empleados CURSOR FOR 
        SELECT ID_TRB, SUELDO, FEC_NAC, AFECTA_IHS, AFECTA_FSV, AFECTA_ISR, afecta_sin
        FROM empleado 
        WHERE EST_TRB = 'S';


    -- =========================================================================
    -- BLOQUE 3: DECLARACIÓN DE HANDLERS
    -- =========================================================================
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
        code = RETURNED_SQLSTATE, msg = MESSAGE_TEXT, v_nombre_columna = column_name;
    END;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_fin_cursor = 1;

    -- =========================================================================
    -- LÓGICA DE PROCESAMIENTO
    -- =========================================================================
      
    SELECT IFNULL(sindicato, 0.00), 
    IFNULL(ipp, 0.00)
    INTO  V_VALOR_SINDICATO, V_VALOR_IPP
    FROM PARAMETRO p
    WHERE p.periodo = p_anio;
    
    SET V_VALOR_SINDICATO = COALESCE(V_VALOR_SINDICATO, 0.00);
    SET V_VALOR_IPP       = COALESCE(V_VALOR_IPP, 0.00);
   
  IF P_TIPO_PLANILLA = 'ANTICIPO' THEN
        -- Validamos si ya existe un anticipo para este mes/año
        SELECT COUNT(*) INTO V_EXISTE_ANTICIPO
        FROM PLANILLA 
        WHERE TIPO_PLANILLA = 'ANTICIPO'
          AND YEAR(FECHA) = YEAR(p_fecha)
          AND MONTH(FECHA) = MONTH(p_fecha);

        IF V_EXISTE_ANTICIPO > 0 THEN
            SET P_SALIDA = -2; 
            LEAVE main_procedure; 
        END IF;
        
        SET P_TIPO_PLANILLA = 'MENSUAL';
        SET V_ANTICIPO = 'ANTICIPO'; 
    END IF;
    
    SET v_dia_pago = DAY(p_fecha);
    IF v_dia_pago <= 15 THEN
        SET v_es_quincena_1 = 1;
    END IF;

    IF p_tipo_planilla = 'MENSUAL' AND v_es_quincena_1 = 0 THEN
        SET v_fecha_inicio = DATE_FORMAT(p_fecha, '%Y-%m-01');
        SET v_fecha_fin = LAST_DAY(p_fecha);
    ELSE
        IF v_es_quincena_1 = 1 THEN
            SET v_fecha_inicio = DATE_FORMAT(p_fecha, '%Y-%m-01');
            SET v_fecha_fin = DATE_FORMAT(p_fecha, '%Y-%m-15');
        ELSE
            SET v_fecha_inicio = DATE_FORMAT(p_fecha, '%Y-%m-16');
            SET v_fecha_fin = LAST_DAY(p_fecha);
        END IF;
    END IF;
    
    SET SQL_SAFE_UPDATES = 0;
    
    DELETE FROM planilla WHERE COD_PLANILLA = p_cod_planilla AND FECHA = p_fecha;
    
    START TRANSACTION;
    OPEN cur_empleados;

    read_loop: LOOP
        FETCH cur_empleados INTO 
            v_id_empleado, 
            v_sueldo_base, 
            v_fecha_nac, 
            v_afecta_ihs, 
            v_afecta_fsv, 
            v_afecta_isr,
            v_afecta_sindicato;
        
        IF v_fin_cursor = 1 THEN
            LEAVE read_loop;
        END IF;

        SET v_edad = YEAR(p_fecha) - YEAR(v_fecha_nac) - (DATE_FORMAT(p_fecha,'%m%d') < DATE_FORMAT(v_fecha_nac,'%m%d'));
        SET v_diario = ROUND(v_sueldo_base / 30, 2);
        SET v_total_labores = 0.00;
        SET v_total_ausencias = 0.00;
        SET v_cuota_prestamos = 0.00; 
        SET v_total_descuentos = 0.00;
        SET v_ihss_calc = 0.00;
        SET v_rap_calc = 0.00;
        SET v_isr_calc = 0.00;

        IF p_tipo_planilla = 'QUINCENAL' OR (p_tipo_planilla = 'MENSUAL' AND v_es_quincena_1 = 1) THEN
            SET v_sueldo_base = ROUND(v_sueldo_base / 2, 2);
        END IF;

        BEGIN
            DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_total_labores = 0.00;
            SELECT IFNULL(SUM(IF(MONTO_LABOR = 0, CANTIDAD_LAB, CANTIDAD_LAB * MONTO_LABOR)), 0.00) 
            INTO v_total_labores FROM mlabores WHERE ID_EMPLEADO = v_id_empleado 
             AND fecha_labor BETWEEN v_fecha_inicio AND v_fecha_fin;
        END;

        IF NOT (p_tipo_planilla = 'MENSUAL' AND v_es_quincena_1 = 1) THEN

            BEGIN
                DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_total_ausencias = 0.00;
                SELECT IFNULL(SUM(MONTO), 0.00) 
                INTO v_total_ausencias FROM ausencias WHERE ID_EMPLEADO = v_id_empleado 
                AND fecha_inicial <= v_fecha_fin AND fecha_final >= v_fecha_inicio;
            END;

            BEGIN
                DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_total_descuentos = 0.00;
                SELECT IFNULL(SUM(IF(CANT_DESCUENTO = 0, MON_DESCUENTO, CANT_DESCUENTO * MON_DESCUENTO)), 0.00) 
                INTO v_total_descuentos FROM mdescuentos WHERE ID_EMPLEADO = v_id_empleado 
                AND fecha_descuento BETWEEN v_fecha_inicio AND v_fecha_fin;
            END;

            BEGIN
                DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_ihss_calc = 0.00;
                IF COALESCE(v_afecta_ihs, 'N') = 'S' THEN
                    SET v_ihss_calc = IFNULL(fn_calcular_ihss(v_sueldo_base, p_anio), 0.00);
                END IF;
            END;

            BEGIN
                DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_rap_calc = 0.00;
                IF COALESCE(v_afecta_fsv, 'N') = 'S' THEN
                    SET v_rap_calc = IFNULL(fn_calcular_rap(v_sueldo_base, p_anio), 0.00);
                END IF;
            END;

           
            BEGIN
                DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_isr_calc = 0.00;
                IF COALESCE(v_afecta_isr, 'N') = 'S' THEN
                    SET v_isr_calc = IFNULL(fn_isr(v_sueldo_base , p_anio, v_edad), 0.00);
                END IF;
            END;
        END IF;
        
		IF COALESCE(v_afecta_sindicato, 'N') = 'S' THEN
			SET v_sindicato_empleado = V_VALOR_SINDICATO;
		ELSE
			SET v_sindicato_empleado = 0.00;
		END IF;
        
        BEGIN 
            DECLARE cur_prestamos CURSOR FOR 
                SELECT ID_PRESTAMO, P_DEB, P_CRED, P_ACT, CUOTA_MES, ID_TIPO_PAGO 
                FROM prestamo 
                WHERE ID_EMPLEADO = v_id_empleado AND ESTADO = 'A'
                AND fecha <= v_fecha_fin;
                
            DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_fin_cursor_pres = 1;
            
            SET v_fin_cursor_pres = 0;
            OPEN cur_prestamos;
            
            loop_prestamos: LOOP
        
                FETCH cur_prestamos INTO v_id_prestamo, v_p_deb, v_p_cred, v_saldo_actual, v_cuota_mes, v_id_tipo_pago;
                
                IF v_fin_cursor_pres = 1 THEN
                    LEAVE loop_prestamos;
                END IF;
                
                SET v_p_cred = IFNULL(v_p_cred, 0.00);
                SET v_p_deb  = IFNULL(v_p_deb, 0.00);
                SET v_saldo_actual = IFNULL(v_saldo_actual, (v_p_deb - v_p_cred));
                
                SET v_aplicar_prestamo = 0; 
                
                IF p_tipo_planilla = 'MENSUAL' THEN
                    IF v_es_quincena_1 = 1 AND v_id_tipo_pago IN (1) THEN
                        SET v_cuota_mes = ROUND(v_cuota_mes / 2, 2);
                        SET v_aplicar_prestamo = 1;
                    ELSEIF v_es_quincena_1 = 0 THEN
                        IF v_id_tipo_pago = 2 THEN
                            SET v_aplicar_prestamo = 1;
                        ELSEIF v_id_tipo_pago = 1 THEN
                            SET v_cuota_mes = ROUND(v_cuota_mes / 2, 2);
                            SET v_aplicar_prestamo = 1;
                        END IF;
                    END IF;
                ELSEIF p_tipo_planilla = 'QUINCENAL' THEN
                    IF v_id_tipo_pago = 1 THEN
                        SET v_cuota_mes = ROUND(v_cuota_mes / 2, 2);
                        SET v_aplicar_prestamo = 1;
                    ELSEIF v_id_tipo_pago = 2 AND v_es_quincena_1 = 0 THEN
                        SET v_aplicar_prestamo = 1;
                    END IF;
                END IF;
                
                IF v_aplicar_prestamo = 1 AND v_saldo_actual > 0 AND v_p_cred < v_p_deb THEN
                    IF v_saldo_actual < v_cuota_mes THEN
                        SET v_cuota_mes = v_saldo_actual;
                    END IF;
                    
                    SET v_cuota_prestamos = v_cuota_prestamos + v_cuota_mes;
                    
                    UPDATE prestamo 
                    SET P_CRED = v_p_cred + v_cuota_mes,
                        p_Act  = v_p_deb - (v_p_cred + v_cuota_mes),
                        ESTADO = IF((v_p_cred + v_cuota_mes) >= v_p_deb, 'P', 'A')
                    WHERE ID_PRESTAMO = v_id_prestamo;
                END IF;
                
            END LOOP loop_prestamos;
            CLOSE cur_prestamos;
        END;

        SET v_salario_bruto = v_sueldo_base + v_total_labores;
       SET v_total_deducciones = COALESCE(v_ihss_calc, 0.00) 
                        + COALESCE(v_rap_calc, 0.00) 
                        + COALESCE(v_isr_calc, 0.00) 
                        + COALESCE(v_total_ausencias, 0.00) 
                        + COALESCE(v_cuota_prestamos, 0.00) 
                        + COALESCE(v_total_descuentos, 0.00) 
                        + COALESCE(v_sindicato_empleado, 0.00) 
                        + COALESCE(V_VALOR_IPP, 0.00);
       SET v_salario_neto = v_salario_bruto - v_total_deducciones;

        IF EXISTS(
            SELECT 1 FROM planilla
            WHERE COD_PLANILLA = p_cod_planilla AND FECHA = p_fecha AND ID_EMPLEADO = v_id_empleado
        ) THEN
            ITERATE read_loop;
        END IF;

        
        INSERT INTO planilla (
            COD_PLANILLA, FECHA, ID_EMPLEADO, SUELDO, DIARIO, 
            LABORES, AUMENTO, SALARIO, IHSS, RAP, 
            ISR, AUSENCIAS, SEPTIMO, DEDUCCIONES, CUOTA_PRESTAMO, 
            DESCUENTOS, SALARIO_NETO, TIPO_PLANILLA
        ) VALUES (
            p_cod_planilla, p_fecha, v_id_empleado, v_sueldo_base, v_diario,
            v_total_labores, 0.00, v_salario_bruto, v_ihss_calc, v_rap_calc,
            v_isr_calc, v_total_ausencias, 0.00, v_total_deducciones,
            v_cuota_prestamos,
            v_total_descuentos, v_salario_neto, IFNULL(V_ANTICIPO, P_TIPO_PLANILLA)
        );

    END LOOP read_loop;

    CLOSE cur_empleados;
    COMMIT;
    SET SQL_SAFE_UPDATES = 1;

   IF code <> '00000' THEN  
        INSERT INTO error_log (MENSAJE, TABLA, CODIGO_ERROR, FECHA_ERROR, NOMBRE_COLUMNA, VALOR_CAMPO) 
        VALUES (msg, 'PLANILLA', code, NOW(), v_nombre_columna, v_valor_campo);
        SET P_SALIDA = -1; 
    ELSE
        SET P_SALIDA = 1;
    END IF;
    
    
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_generar_planilla_prueba`(
    IN p_cod_planilla VARCHAR(10),
    IN p_fecha DATE,
    IN p_anio INT,
    IN p_tipo_planilla VARCHAR(1) -- 'M' = Mensual, 'Q' = Quincenal
)
BEGIN
    -- ========================================================================
    -- 1. DECLARACIÓN DE VARIABLES Y HANDLERS
    -- ========================================================================
    DECLARE v_fin_cursor INT DEFAULT 0;
    
    -- Variables de empleado
    DECLARE v_id_empleado INT;
    DECLARE v_sueldo_mensual_completo DECIMAL(17,2);
    DECLARE v_fecha_nac DATE;
    DECLARE v_edad INT;
    DECLARE v_afecta_ihs VARCHAR(1);
    DECLARE v_afecta_fsv VARCHAR(1);
    DECLARE v_afecta_isr VARCHAR(1);
    
    -- Variables de cálculo
    DECLARE v_factor_prorrateo DECIMAL(5,2);      -- 0.5 o 1
    DECLARE v_sueldo_periodo DECIMAL(17,2);
    DECLARE v_diario DECIMAL(17,2);
    DECLARE v_total_labores DECIMAL(17,2);
    DECLARE v_total_ausencias DECIMAL(17,2);
    DECLARE v_total_descuentos DECIMAL(17,2);
    DECLARE v_cuota_prestamos DECIMAL(17,2);
    DECLARE v_ihss_calc DECIMAL(17,2);
    DECLARE v_rap_calc DECIMAL(17,2);
    DECLARE v_isr_calc DECIMAL(17,2);
    DECLARE v_salario_bruto DECIMAL(17,2);
    DECLARE v_total_deducciones DECIMAL(17,2);
    DECLARE v_salario_neto DECIMAL(17,2);
    
    -- Fechas del período
    DECLARE v_fecha_inicio DATE;
    DECLARE v_fecha_fin DATE;
    DECLARE v_es_primera_quincena INT DEFAULT 0;
    
    -- Para restaurar safe_updates
    DECLARE v_original_safe_updates INT;
    
    -- Cursor principal
    DECLARE cur_empleados CURSOR FOR 
        SELECT ID_TRB, SUELDO, FEC_NAC, AFECTA_IHS, AFECTA_FSV, AFECTA_ISR
        FROM empleado 
        WHERE EST_TRB = 'S';
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET v_fin_cursor = 1;
    
    -- Handler para restaurar safe_updates en caso de error
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET SQL_SAFE_UPDATES = v_original_safe_updates;
        RESIGNAL;
    END;
    
    -- ========================================================================
    -- 2. CONFIGURACIÓN INICIAL Y LIMPIEZA
    -- ========================================================================
    -- Guardar estado original de SQL_SAFE_UPDATES
    SELECT @@SQL_SAFE_UPDATES INTO v_original_safe_updates;
    SET SQL_SAFE_UPDATES = 0;
    
    -- Eliminar registros previos de la misma planilla (forma simple y eficiente)
    DELETE FROM planilla WHERE COD_PLANILLA = p_cod_planilla AND FECHA = p_fecha;
    
    -- Determinar si es primera quincena (día 15 o anterior)
    SET v_es_primera_quincena = IF(DAY(p_fecha) <= 15, 1, 0);
    
    -- Calcular factor de prorrateo
    -- Mensual y es fin de mes (último día o día 15? Según lógica original: solo fin de mes completo)
    -- Para mensual: si es anticipo (15) -> factor 0.5; si es fin de mes -> factor 1
    -- Para quincenal: siempre factor 0.5
    IF p_tipo_planilla = 'M' THEN
        IF v_es_primera_quincena = 1 THEN
            SET v_factor_prorrateo = 0.5;   -- Anticipo del 15
        ELSE
            SET v_factor_prorrateo = 1.0;   -- Liquidación mensual completa
        END IF;
    ELSE -- 'Q'
        SET v_factor_prorrateo = 0.5;
    END IF;
    
    -- Calcular fechas de inicio y fin del período según tipo y fecha de pago
    IF p_tipo_planilla = 'M' AND v_es_primera_quincena = 0 THEN
        -- Mensual completo: desde 1ro hasta último día del mes
        SET v_fecha_inicio = DATE_FORMAT(p_fecha, '%Y-%m-01');
        SET v_fecha_fin = LAST_DAY(p_fecha);
    ELSE
        -- Quincena o anticipo mensual
        IF v_es_primera_quincena = 1 THEN
            SET v_fecha_inicio = DATE_FORMAT(p_fecha, '%Y-%m-01');
            SET v_fecha_fin = DATE_FORMAT(p_fecha, '%Y-%m-15');
        ELSE
            SET v_fecha_inicio = DATE_FORMAT(p_fecha, '%Y-%m-16');
            SET v_fecha_fin = LAST_DAY(p_fecha);
        END IF;
    END IF;
    
    -- ========================================================================
    -- 3. PRECÁLCULO DE PRÉSTAMOS (elimina cursor anidado)
    -- ========================================================================
    DROP TEMPORARY TABLE IF EXISTS tmp_prestamo_empleado;
    CREATE TEMPORARY TABLE tmp_prestamo_empleado (
        ID_EMPLEADO INT PRIMARY KEY,
        TOTAL_CUOTA DECIMAL(17,2) NOT NULL DEFAULT 0.00
    ) ENGINE=MEMORY;
    
    -- Insertar el total a descontar por empleado en esta planilla
    INSERT INTO tmp_prestamo_empleado (ID_EMPLEADO, TOTAL_CUOTA)
    SELECT 
        p.ID_EMPLEADO,
        SUM(
            CASE
                -- Lógica de cobro según tipo de planilla, tipo de préstamo y quincena
                WHEN p_tipo_planilla = 'M' AND v_es_primera_quincena = 1 AND p.ID_TIPO_PAGO = 1 THEN ROUND(p.CUOTA_MES / 2, 2)
                WHEN p_tipo_planilla = 'M' AND v_es_primera_quincena = 0 AND p.ID_TIPO_PAGO = 2 THEN p.CUOTA_MES
                WHEN p_tipo_planilla = 'M' AND v_es_primera_quincena = 0 AND p.ID_TIPO_PAGO = 1 THEN ROUND(p.CUOTA_MES / 2, 2)
                WHEN p_tipo_planilla = 'Q' AND p.ID_TIPO_PAGO = 1 THEN ROUND(p.CUOTA_MES / 2, 2)
                WHEN p_tipo_planilla = 'Q' AND p.ID_TIPO_PAGO = 2 AND v_es_primera_quincena = 0 THEN p.CUOTA_MES
                ELSE 0
            END
        ) AS cuota_a_deducir
    FROM prestamo p
    WHERE p.ESTADO = 'A'
      AND IFNULL(p.P_ACT, (p.P_DEB - IFNULL(p.P_CRED,0))) > 0
      AND IFNULL(p.P_CRED,0) < p.P_DEB
    GROUP BY p.ID_EMPLEADO;
    
    -- Actualizar la tabla prestamo: sumar el abono y recalcular estado
    -- (Se hace en una sola sentencia usando la tabla temporal)
    UPDATE prestamo p
    JOIN (
        SELECT 
            ID_PRESTAMO,
            ID_EMPLEADO,
            ID_TIPO_PAGO,
            CUOTA_MES,
            P_DEB,
            P_CRED,
            CASE
                WHEN p_tipo_planilla = 'M' AND v_es_primera_quincena = 1 AND ID_TIPO_PAGO = 1 THEN ROUND(CUOTA_MES / 2, 2)
                WHEN p_tipo_planilla = 'M' AND v_es_primera_quincena = 0 AND ID_TIPO_PAGO = 2 THEN CUOTA_MES
                WHEN p_tipo_planilla = 'M' AND v_es_primera_quincena = 0 AND ID_TIPO_PAGO = 1 THEN ROUND(CUOTA_MES / 2, 2)
                WHEN p_tipo_planilla = 'Q' AND ID_TIPO_PAGO = 1 THEN ROUND(CUOTA_MES / 2, 2)
                WHEN p_tipo_planilla = 'Q' AND ID_TIPO_PAGO = 2 AND v_es_primera_quincena = 0 THEN CUOTA_MES
                ELSE 0
            END AS cuota_real
        FROM prestamo
        WHERE ESTADO = 'A'
    ) AS cuotas ON p.ID_PRESTAMO = cuotas.ID_PRESTAMO
    SET 
        p.P_CRED = IFNULL(p.P_CRED,0) + cuotas.cuota_real,
        p.P_ACT  = p.P_DEB - (IFNULL(p.P_CRED,0) + cuotas.cuota_real),
        p.ESTADO = IF((IFNULL(p.P_CRED,0) + cuotas.cuota_real) >= p.P_DEB, 'I', 'A')
    WHERE cuotas.cuota_real > 0;
    
    -- ========================================================================
    -- 4. BUCLE PRINCIPAL DE EMPLEADOS (con consultas optimizadas por rango)
    -- ========================================================================
    OPEN cur_empleados;
    
    read_loop: LOOP
        FETCH cur_empleados INTO 
            v_id_empleado, 
            v_sueldo_mensual_completo, 
            v_fecha_nac, 
            v_afecta_ihs, 
            v_afecta_fsv, 
            v_afecta_isr;
        
        IF v_fin_cursor = 1 THEN
            LEAVE read_loop;
        END IF;
        
        -- Edad al momento del pago
        SET v_edad = TIMESTAMPDIFF(YEAR, v_fecha_nac, p_fecha);
        
        -- Sueldo del período (aplicando factor de prorrateo)
        SET v_sueldo_periodo = ROUND(v_sueldo_mensual_completo * v_factor_prorrateo, 2);
        SET v_diario = ROUND(v_sueldo_mensual_completo / 30, 2);
        
        -- ================================================================
        -- 4.1 Labores (bonificaciones, horas extras) dentro del período
        -- ================================================================
        SELECT IFNULL(SUM(IF(CANTIDAD_LAB = 0, MONTO_LABOR, CANTIDAD_LAB * MONTO_LABOR)), 0.00)
        INTO v_total_labores
        FROM mlabores
        WHERE ID_EMPLEADO = v_id_empleado
          AND fecha_labor BETWEEN v_fecha_inicio AND v_fecha_fin;
        
        -- ================================================================
        -- 4.2 Ausencias en el período (ya viene como monto por ausencia)
        -- ================================================================
        SELECT IFNULL(SUM(MONTO), 0.00)
        INTO v_total_ausencias
        FROM ausencias
        WHERE ID_EMPLEADO = v_id_empleado
          AND fecha_inicial <= v_fecha_fin
          AND fecha_final >= v_fecha_inicio;
        
        -- ================================================================
        -- 4.3 Descuentos comerciales varios en el período
        -- ================================================================
        SELECT IFNULL(SUM(IF(CANT_DESCUENTO = 0, MON_DESCUENTO, CANT_DESCUENTO * MON_DESCUENTO)), 0.00)
        INTO v_total_descuentos
        FROM mdescuentos
        WHERE ID_EMPLEADO = v_id_empleado
          AND fecha_descuento BETWEEN v_fecha_inicio AND v_fecha_fin;
        
        -- ================================================================
        -- 4.4 Leyes sociales e ISR (SIEMPRE sobre sueldo mensual completo)
        -- ================================================================
        -- Solo se calculan en planilla mensual (fin de mes) o quincenal (ambas fases)
        -- En anticipo mensual (M + primera quincena) NO se aplican
        IF NOT (p_tipo_planilla = 'M' AND v_es_primera_quincena = 1) THEN
            IF v_afecta_ihs = 'S' THEN
                SET v_ihss_calc = IFNULL(fn_calcular_ihss(v_sueldo_mensual_completo, p_anio), 0.00);
            END IF;
            IF v_afecta_fsv = 'S' THEN
                SET v_rap_calc = IFNULL(fn_calcular_rap(v_sueldo_mensual_completo, p_anio), 0.00);
            END IF;
            IF v_afecta_isr = 'S' THEN
                SET v_isr_calc = IFNULL(fn_isr(v_sueldo_mensual_completo, p_anio, v_edad), 0.00);
            END IF;
            
            -- Aplicar factor de prorrateo a los resultados
            SET v_ihss_calc = ROUND(v_ihss_calc * v_factor_prorrateo, 2);
            SET v_rap_calc  = ROUND(v_rap_calc  * v_factor_prorrateo, 2);
            SET v_isr_calc  = ROUND(v_isr_calc  * v_factor_prorrateo, 2);
        ELSE
            SET v_ihss_calc = 0.00;
            SET v_rap_calc  = 0.00;
            SET v_isr_calc  = 0.00;
        END IF;
        
        -- ================================================================
        -- 4.5 Cuota de préstamos (desde tabla temporal precalculada)
        -- ================================================================
        SELECT IFNULL(TOTAL_CUOTA, 0.00) INTO v_cuota_prestamos
        FROM tmp_prestamo_empleado
        WHERE ID_EMPLEADO = v_id_empleado;
        
        -- ================================================================
        -- 4.6 Totales finales e inserción
        -- ================================================================
        SET v_salario_bruto = v_sueldo_periodo + v_total_labores;
        SET v_total_deducciones = v_ihss_calc + v_rap_calc + v_isr_calc 
                                + v_total_ausencias + v_cuota_prestamos + v_total_descuentos;
        SET v_salario_neto = v_salario_bruto - v_total_deducciones;
        
        INSERT INTO planilla (
            COD_PLANILLA, FECHA, ID_EMPLEADO, SUELDO, DIARIO, 
            LABORES, AUMENTO, SALARIO, IHSS, RAP, 
            ISR, AUSENCIAS, SEPTIMO, DEDUCCIONES, CUOTA_PRESTAMO, 
            DESCUENTOS, SALARIO_NETO, TIPO_PLANILLA
        ) VALUES (
            p_cod_planilla, p_fecha, v_id_empleado, v_sueldo_periodo, v_diario,
            v_total_labores, 0.00, v_salario_bruto, v_ihss_calc, v_rap_calc,
            v_isr_calc, v_total_ausencias, 0.00, v_total_deducciones, v_cuota_prestamos,
            v_total_descuentos, v_salario_neto, 
            IF(p_tipo_planilla = 'M', 'MENSUAL', 'QUINCENAL')
        );
        
    END LOOP read_loop;
    
    CLOSE cur_empleados;
    
    -- ========================================================================
    -- 5. LIMPIEZA Y RESULTADO
    -- ========================================================================
    DROP TEMPORARY TABLE IF EXISTS tmp_prestamo_empleado;
    SET SQL_SAFE_UPDATES = v_original_safe_updates;
    
    SELECT * FROM planilla WHERE COD_PLANILLA = p_cod_planilla AND FECHA = p_fecha;
    
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_obtener_retenciones_mensuales`(
    IN p_anio INT,
    IN p_mes INT
)
BEGIN
    SELECT 
        e.RTN AS RTN,
        (SUM(p.SALARIO) * 12) AS Ingresos_Brutos_Anuales,
        (SUM(p.SALARIO) - SUM(p.IHSS + p.RAP)) AS Importe_Mensual_Retencion,
        
        -- AGREGADO SUM() AQUI:
        SUM(p.DEDUCCIONES) AS Deducciones_Mensuales,
        
        -- AGREGADO SUM() AQUI:
        SUM(p.ISR) AS Retenido

    FROM planilla p
    INNER JOIN empleado e ON p.ID_EMPLEADO = e.ID_TRB
    WHERE YEAR(p.FECHA) = p_anio 
      AND MONTH(p.FECHA) = p_mes
    GROUP BY 
        e.RTN, 
        e.NOM_TRB, 
        YEAR(p.FECHA), 
        MONTH(p.FECHA)
    ORDER BY 
        e.NOM_TRB ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_prestamos_descriptivos_empleado`(IN p_filtro int)
BEGIN
  SELECT 
    E.COD_TRB AS 'CODIGO_EMPLEADO',
    E.NOM_TRB AS 'NOMBRE_EMPLEADO',
    P.CODIGO AS 'NUMERO_PRESTAMO',
    P.FECHA AS 'FECHA_OTORGADO',
    P.DESCRIPCION AS 'DESCRIPCION_PRESTAMO',
    P.MONTO AS 'MONTO_ORIGINAL',
    P.CUOTA_MES AS 'MONTO_CUOTA',
    P.TIEMPO AS 'PLAZO_MESES',
    IFNULL(P.P_ANT, 0.00) AS 'SALDO_ANTERIOR',
    IFNULL(P.P_DEB, 0.00) AS 'TOTAL_DEBITADO',
    IFNULL(P.P_CRED, 0.00) AS 'TOTAL_PAGADO_CREDITOS',
    IFNULL(P.P_ACT, 0.00) AS 'SALDO_ACTUAL',
    CASE P.ESTADO 
        WHEN 'A' THEN 'ACTIVO' 
        WHEN 'P' THEN 'PAGADO' 
        ELSE 'CANCELADO' 
    END AS 'ESTADO'
	FROM prestamo P
		INNER JOIN empleado E ON P.ID_EMPLEADO = p_filtro
		WHERE e.ID_TRB = p_filtro
		ORDER BY NOM_TRB ASC, P.FECHA DESC;
END ;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_registrar_bitacora`(
    IN p_usuario VARCHAR(10),
    IN p_accion VARCHAR(50),
    IN p_tabla_afectada VARCHAR(100),
    IN p_descripcion TEXT,
    IN p_codigo_usuario varchar(150)
)
BEGIN
    -- Declaramos una variable interna para recuperar el nombre completo del usuario
    DECLARE v_nombre_usuario VARCHAR(150);
    
    -- Insertamos el registro completo en la tabla de auditoría
    INSERT INTO bitacora (
        nombre_usuario,
        accion,
        tabla_afectada,
        descripcion,
        fecha_registro
    )
    VALUES (
        p_usuario,
        UPPER(p_accion), -- Guardamos la acción siempre en mayúsculas (INSERT, UPDATE, DELETE)
        p_tabla_afectada,
        p_descripcion,
        NOW() 
    );

END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_categoria`(
    IN p_filtro int
)
BEGIN
    SELECT 
      d.NOM_CAT AS `DEPARTAMENTO`,
      e.ID_TRB AS `CODIGO EMPLEADO`,
	  e.NOM_TRB AS `NOMBRE EMPLEADO`, 
      e.fecha_inicio  AS `FECHA DE INGRESO`,
      e.puesto_trabajo  AS `PUESTO DE TRABAJO`,
       e.sueldo  AS `SUELDO ASIGNADO`
    FROM empleado e
    INNER JOIN categoria d ON e.ID_CAT= d.ID_CAT
    where (p_filtro IS NULL OR d.ID_CAT = p_filtro)
    ORDER BY d.NOM_CAT ASC, e.NOM_TRB ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_departamento`(
    IN p_filtro  int
)
BEGIN
    SELECT 
      d.NOM_DEP AS `DEPARTAMENTO`,
      e.ID_TRB AS `CODIGO EMPLEADO`,
	  e.NOM_TRB AS `NOMBRE EMPLEADO`, 
      e.fecha_inicio  AS `FECHA DE INGRESO`,
      e.puesto_trabajo  AS `PUESTO DE TRABAJO`,
       e.sueldo  AS `SUELDO ASIGNADO`
    FROM empleado e
    INNER JOIN departamento d ON e.ID_DEP= d.ID_DEP
    where (p_filtro IS NULL OR d.ID_DEP = p_filtro)
    ORDER BY d.NOM_DEP ASC, e.NOM_TRB ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_general_categorias`()
BEGIN
	SELECT 
		c.COD_CAT AS 'CODIGO',
		c.NOM_CAT AS 'CATEGORIA',
		c.SAL_INI AS 'SALARIO_MINIMO',
		c.SAL_FIN AS 'SALARIO_MAXIMO',
		(c.SAL_FIN - c.SAL_INI) AS 'AMPLITUD',
		COUNT(e.ID_TRB) AS 'TOTAL_EMPLEADOS',
		IFNULL(ROUND(AVG(e.SUELDO), 2), 0.00) AS 'SUELDO_PROMEDIO_REAL'
	FROM n1.categoria c
	LEFT JOIN n1.empleado e ON c.ID_CAT = e.ID_CAT AND e.ESTADO= 'A'
	GROUP BY c.ID_CAT, c.COD_CAT, c.NOM_CAT, c.SAL_INI, c.SAL_FIN
	ORDER BY c.SAL_INI ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_general_deducciones`( IN P_FILTRO int)
BEGIN
SELECT 
    p.COD_PLANILLA AS 'CODPLANILLA',
    p.FECHA AS 'FECHA_EMISION',
    e.COD_TRB AS 'CODIGO_EMPLEADO',
    e.NOM_TRB AS 'NOMBRE_EMPLEADO',
    IFNULL(p.IHSS, 0.00) AS 'IHSS',
    IFNULL(p.RAP, 0.00) AS 'RAP',
    IFNULL(p.ISR, 0.00) AS 'ISR',
    IFNULL(p.AUSENCIAS, 0.00) AS 'AUSENCIAS',
    IFNULL(p.CUOTA_PRESTAMO, 0.00) AS 'PRESTAMOS',
    IFNULL(p.DESCUENTOS, 0.00) AS 'OTROS_DESCUENTOS',
    IFNULL(p.DEDUCCIONES, 0.00) AS 'TOTAL_DEDUCCIONES',
    IFNULL(p.SUELDO, 0.00) AS 'SUELDO_BRUTO',
    IFNULL(p.SALARIO_NETO, 0.00) AS 'SUELDO_NETO',
    p.TIPO_PLANILLA AS 'TIPO_PLANILLA'
FROM planilla p
INNER JOIN empleado e ON p.ID_EMPLEADO = e.ID_TRB
where (P_FILTRO IS NULL OR P_FILTRO = 0 OR e.ID_TRB = P_FILTRO)
ORDER BY p.FECHA DESC, e.NOM_TRB ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_general_empleados`()
BEGIN
	SELECT 
		E.COD_TRB AS 'CODIGO',
		E.NOM_TRB AS 'EMPLEADO',
		E.IDEN_TRB AS 'IDENTIDAD',
		E.RTN AS 'RTN',
		E.PUESTO_TRABAJO AS 'PUESTO',
		D.NOM_DEP AS 'DEPARTAMENTO',        
		TE.DESCRIPCION AS 'TIPO_EMPLEADO',         
		FP.DESCRIPCION AS 'FORMA_DE_PAGO',     
		E.FECHA_CONTRATACION AS 'F_CONTRATACION',
		E.SUELDO AS 'SUELDO_BASE',
		E.BANCOS AS 'BANCO',
		E.NCUENTA AS 'NO_CUENTA',
		CASE E.ESTADO WHEN 'A' THEN 'ACTIVO' ELSE 'INACTIVO' END AS 'ESTADO'
	FROM N1.EMPLEADO E
	LEFT JOIN N1.DEPARTAMENTO D ON E.ID_DEP = D.ID_DEP
	LEFT JOIN N1.TIPO_EMPLEADO TE ON E.ID_TIPO_EMPLEADO = TE.ID_TIPO_EMPLEADO
	LEFT JOIN N1.FORMA_PAGO FP ON E.ID_TIPO_PAGO = FP.ID_TIPO_PAGO
	WHERE E.ESTADO = 'A';
END ;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_labores`(
    IN p_filtro int
)
BEGIN
    SELECT 
      e.ID_TRB AS `CODIGO_EMPLEADO`,
	  e.NOM_TRB AS `NOMBRE_EMPLEADO`,
	  CASE tp.descripcion 
            WHEN 'D' THEN 'Definido por el usuario'
            WHEN 'F' THEN 'Por Factor'
            WHEN 'H' THEN 'Por Hora'
            WHEN 'V' THEN 'Por Valor'
            ELSE tp.descripcion -- Por si acaso viene un valor diferente ya registrado
        END AS `TIPO_LABOR`,
	  d.monto_labor as  `VALOR_LABOR`,
      d.cantidad_lab as  `CANTIDAD_EJECUTADA`,
	  d.fecha_labor  AS `FECHA_LABOR`,
      IFNULL((IF(MONTO_LABOR = 0, CANTIDAD_LAB, CANTIDAD_LAB * MONTO_LABOR)), 0.00) as `MONTO_LABOR`
    FROM empleado e
    INNER JOIN mlabores d ON e.ID_TRB= d.ID_EMPLEADO
    INNER JOIN tipo_pago tp on tp.id_tipo_pago = d.id_tipo_pago
    where (P_FILTRO IS NULL OR P_FILTRO = 0 OR e.ID_TRB = p_filtro)
    ORDER BY  e.NOM_TRB ASC;
END ;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_liquidacion_detallado`(
    IN p_id_empleado INT,
    IN p_fecha_fin DATE
)
BEGIN
    -- Declaramos una variable interna para capturar la fecha en que inició el trabajador
    DECLARE v_fecha_ingreso DATE;
    
    -- Buscamos la fecha de contratación del empleado
    SELECT `FECHA_CONTRATACION` INTO v_fecha_ingreso
    FROM `empleado`
    WHERE `ID_TRB` = p_id_empleado;

    -- Si por algún motivo no tiene fecha de contratación, usamos una por defecto para evitar errores
    IF v_fecha_ingreso IS NULL THEN
        SET v_fecha_ingreso = '2000-01-01';
    END IF;


    -- =========================================================================
    -- 1. SECCIÓN DE INGRESOS: Salario Base
    -- =========================================================================
    SELECT 
        e.`COD_TRB`, 
        e.`NOM_TRB`,
        e.`PUESTO_TRABAJO` AS PUESTO_TRABAJO,
        (SELECT d.`NOM_DEP` FROM `departamento` d WHERE d.`ID_DEP` = e.`ID_DEP`) AS DEPTO,
		(SELECT d.`COD_DEP` FROM `departamento` d WHERE d.`ID_DEP` = e.`ID_DEP`) AS COD_DEPTO,
       e.`SUELDO`,
        'SAL-01' AS CODIGO,
        'SALARIO BASE' AS NOMBRE_MOVIMIENTO,
        'SALARIO' AS TIPO_MOVIMIENTO,
        p_fecha_fin AS FECHA,
        e.`SUELDO` AS MONTO
    FROM `empleado` e
    WHERE e.`ID_TRB` = p_id_empleado

    UNION ALL

    -- =========================================================================
    -- 2. SECCIÓN DE DEDUCCIONES: Préstamos Activos
    -- =========================================================================
    SELECT 
        e.`COD_TRB`, e.`NOM_TRB`, e.`PUESTO_TRABAJO`, NULL,NULL, e.`SUELDO`,
        p.`CODIGO` AS CODIGO,
        p.`DESCRIPCION` AS NOMBRE_MOVIMIENTO,
        'DEDUCCION' AS TIPO_MOVIMIENTO,
        p.`FECHA` AS FECHA,
        p.`CUOTA_MES` AS MONTO
    FROM `prestamo` p
    INNER JOIN `empleado` e ON p.`ID_EMPLEADO` = e.`ID_TRB`
    WHERE p.`ID_EMPLEADO` = p_id_empleado 
      AND p.`ESTADO` = 'A'
      -- Filtra los préstamos desde que inició hasta la fecha de corte
      AND p.`FECHA` BETWEEN v_fecha_ingreso AND p_fecha_fin

    UNION ALL

    -- =========================================================================
    -- 3. SECCIÓN DE DEDUCCIONES: Descuentos Comerciales
    -- =========================================================================
    SELECT 
        e.`COD_TRB`, e.`NOM_TRB`, e.`PUESTO_TRABAJO`, NULL, NULL,e.`SUELDO`,
        CAST(m.`ID_DESCUENTO` AS CHAR) AS CODIGO,
        m.`DESCRIPCION_DESCUENTO` AS NOMBRE_MOVIMIENTO,
        'DEDUCCION' AS TIPO_MOVIMIENTO,
        m.`FECHA_DESCUENTO` AS FECHA,
        IF(m.`CANT_DESCUENTO` = 0, m.`MON_DESCUENTO`, m.`CANT_DESCUENTO` * m.`MON_DESCUENTO`) AS MONTO
    FROM `mdescuentos` m
    INNER JOIN `empleado` e ON m.`ID_EMPLEADO` = e.`ID_TRB`
    WHERE m.`ID_EMPLEADO` = p_id_empleado
      -- Rango automático usando la fecha interna del trabajador
      AND m.`FECHA_DESCUENTO` BETWEEN v_fecha_ingreso AND p_fecha_fin

    UNION ALL

    -- =========================================================================
    -- 4. SECCIÓN DE DEDUCCIONES: Ausencias
    -- =========================================================================
    SELECT 
        e.`COD_TRB`, e.`NOM_TRB`, e.`PUESTO_TRABAJO`, NULL,NULL, e.`SUELDO`,
        CAST(a.`ID_TIPO_AUSENCIA` AS CHAR) AS CODIGO,
        'DEDUCCION POR AUSENCIA' AS NOMBRE_MOVIMIENTO,
        'DEDUCCION' AS TIPO_MOVIMIENTO,
        a.`FECHA_INICIAL` AS FECHA,
        a.`MONTO` AS MONTO
    FROM `ausencias` a
    INNER JOIN `empleado` e ON a.`ID_EMPLEADO` = e.`ID_TRB`
    WHERE a.`ID_EMPLEADO` = p_id_empleado
      AND a.`FECHA_INICIAL` <= p_fecha_fin 
      AND a.`FECHA_FINAL` >= v_fecha_ingreso;

END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_nomina_categoria`(
    IN P_FILTRO int
)
BEGIN
    SELECT 
      d.NOM_CAT AS `CATEGORIA`,
      e.ID_TRB AS `CODIGO_EMPLEADO`,
	  e.NOM_TRB AS `NOMBRE_EMPLEADO`, 
      e.fecha_inicio  AS `FECHA_DE_INGRESO`,
      e.puesto_trabajo  AS `PUESTO_DE_TRABAJO`,
       e.sueldo  AS `SUELDO_ASIGNADO`
    FROM empleado e
    INNER JOIN categoria d ON e.ID_DEP= d.ID_CAT
    where (P_FILTRO IS NULL OR P_FILTRO = 0 OR d.ID_CAT = P_FILTRO)
    ORDER BY d.NOM_CAT ASC, e.NOM_TRB ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_nomina_departamento`(
    IN P_FILTRO  int
)
BEGIN
    SELECT 
      d.NOM_DEP AS `DEPARTAMENTO`,
      e.ID_TRB AS `CODIGO_EMPLEADO`,
	  e.NOM_TRB AS `NOMBRE_EMPLEADO`, 
      e.fecha_inicio  AS `FECHA_DE_INGRESO`,
      e.puesto_trabajo  AS `PUESTO_DE_TRABAJO`,
       e.sueldo  AS `SUELDO_ASIGNADO`
    FROM empleado e
    INNER JOIN departamento d ON e.ID_DEP= d.ID_DEP
   WHERE (P_FILTRO IS NULL OR P_FILTRO = 0 OR d.ID_DEP = P_FILTRO)
    ORDER BY d.NOM_DEP ASC, e.NOM_TRB ASC;
END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_reporte_nomina_libro_salarios`( IN p_anio int, IN p_mes int, IN p_tipo varchar(150) )
BEGIN
   SELECT
		e.ID_TRB AS `CODIGO_EMPLEADO`,
		e.NOM_TRB AS `NOMBRE_EMPLEADO`,
		e.sueldo AS `SUELDO_NOMINAL`,
		p.LABORES AS `OTROS`,
		 p.SALARIO AS `TOTAL_PERCIBIDO`,
	-- 3. Días No Trabajados (Ausencias divididas por quincena según la fecha de la planilla)
		(
			SELECT IFNULL(SUM(a.NUMERO_DIAS_TRABAJADOS), 0) 
			FROM ausencias a 
			WHERE a.ID_EMPLEADO = e.ID_TRB  
			  AND YEAR(a.FECHA_INICIAL) = p_anio
			  AND MONTH(a.FECHA_INICIAL) = p_mes
			  AND a.FECHA_INICIAL <= p.FECHA -- La ausencia tuvo que pasar antes o el mismo día de la planilla
			  AND a.FECHA_INICIAL > CASE 
										WHEN DAY(p.FECHA) <= 15 THEN DATE_FORMAT(p.FECHA, '%Y-%m-01') -- Si es anticipo, desde el día 1
										ELSE DATE_FORMAT(p.FECHA, '%Y-%m-15') -- Si es fin de mes, solo del 16 en adelante
									END
		) AS `DIAS`,
		p.AUSENCIAS AS `MONTO`,

		-- 4. Deducciones de Ley y Retenciones
		p.IHSS AS `IHSS`,
		p.RAP AS `RAP`,
		p.ISR AS `ISR`,
		-- 5. Otras Deducciones (Suma de Descuentos + Préstamos)
		(p.DESCUENTOS + p.CUOTA_PRESTAMO) AS `OTRAS_DEDUCCIONES`,

		( p.IHSS+ p.RAP+ p.ISR +p.DESCUENTOS + p.CUOTA_PRESTAMO ) AS `TOTAL_DEDUCCIONES`,
		-- 6. Neto Líquido
		p.SALARIO_NETO AS `NETO_A_RECIBIR`
		FROM planilla p
		INNER JOIN empleado e ON p.ID_EMPLEADO = e.ID_TRB
		WHERE YEAR(p.FECHA) = p_anio 
		AND MONTH(p.FECHA) = p_mes
        AND (
            (p_tipo = 'PQUINCENA' AND DAY(P.FECHA) <= 15 AND P.TIPO_PLANILLA LIKE '%QUINCENA%') OR
            (p_tipo = 'SQUINCENA' AND DAY(P.FECHA) > 15  AND P.TIPO_PLANILLA LIKE '%QUINCENA%') OR
            (p_tipo = 'ANTICIPO'  AND DAY(P.FECHA) <= 15 AND P.TIPO_PLANILLA LIKE '%ANTICIPO%') OR
            (p_tipo = 'MENSUAL'   AND  P.TIPO_PLANILLA LIKE '%MENSUAL%') 
          );

END ;

CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_resumen_prestamos_empleados`(IN p_filtro int)
BEGIN
	SELECT 
		E.COD_TRB AS 'CODIGO_EMPLEADO',
		E.NOM_TRB AS 'NOMBRE_EMPLEADO',
		COUNT(P.ID_PRESTAMO) AS 'CANTIDAD_PRESTAMOS_TOTALES',
		SUM(CASE WHEN P.ESTADO = 'A' THEN 1 ELSE 0 END) AS 'PRESTAMOS_ACTIVOS',
		SUM(P.MONTO) AS 'TOTAL_PRESTADO_HISTORICO',
		SUM(IFNULL(P.P_CRED, 0.00)) AS 'TOTAL_PAGADO_A_LA_FECHA',
		SUM(IFNULL(P.P_ACT, 0.00)) AS 'DEUDA_TOTAL_PENDIENTE',
		SUM(CASE WHEN P.ESTADO = 'A' THEN IFNULL(P.CUOTA_mes, 0.00) ELSE 0.00 END) AS 'RETENCION_MENSUAL_REQUERIDA'
	FROM empleado E
	INNER JOIN prestamo P ON E.ID_TRB = P.ID_EMPLEADO 
	WHERE ID_TRB = p_filtro
	GROUP BY E.ID_TRB, E.COD_TRB, E.NOM_TRB
	HAVING DEUDA_TOTAL_PENDIENTE > 0 
	ORDER BY E.NOM_TRB ASC;
END ;
