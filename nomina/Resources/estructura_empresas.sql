CREATE DATABASE IF NOT EXISTS sistema_nomina
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE sistema_nomina;


SET FOREIGN_KEY_CHECKS = 0;

CREATE TABLE `bitacora` (
  `id_bitacora` int NOT NULL AUTO_INCREMENT,
  `nombre_usuario` varchar(150) NOT NULL,
  `accion` varchar(50) NOT NULL,
  `tabla_afectada` varchar(100) NOT NULL,
  `descripcion` text NOT NULL,
  `fecha_registro` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_bitacora`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `tipo_aumento` (
  `TIPO_AUMENTO_ID` int NOT NULL AUTO_INCREMENT,
  `DESCRIPCION` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`TIPO_AUMENTO_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `tipo_aumento` VALUES (1,'Monto Fijo'),(2,'Porcentaje');

CREATE TABLE `tipo_empleado` (
  `ID_TIPO_EMPLEADO` int NOT NULL AUTO_INCREMENT,
  `DESCRIPCION` varchar(1000) NOT NULL,
  PRIMARY KEY (`ID_TIPO_EMPLEADO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `tipo_empleado` VALUES (1,'PERMANENTE'),(2,'EVENTUAL');

CREATE TABLE `tipo_ausencia` (
  `id_tipo_ausencia` int NOT NULL AUTO_INCREMENT,
  `descripcion_corta` varchar(45) DEFAULT NULL,
  `descripcion_larga` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_tipo_ausencia`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `tipo_ausencia` VALUES (1,'I','Incapacidad'),(2,'V','Vacaciones'),(3,'N','No se presento'),(4,'P','Permisos'),(5,'PS','Permisos sin goce de sueldo');

CREATE TABLE `tipo_pago` (
  `ID_TIPO_PAGO` int NOT NULL AUTO_INCREMENT,
  `DESCRIPCION` varchar(200) NOT NULL,
  PRIMARY KEY (`ID_TIPO_PAGO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `tipo_pago` VALUES (1,'D'),(2,'F'),(3,'H'),(4,'V');

CREATE TABLE `tipo_pago_empleado` (
  `id_tipo_pago_empleado` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id_tipo_pago_empleado`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;
INSERT INTO `tipo_pago_empleado` VALUES (1,'MENSUAL');

CREATE TABLE `tipo_jornada` (
  `id_tipo_jornada` int NOT NULL AUTO_INCREMENT,
  `descripcion` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_tipo_jornada`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `tipo_jornada` VALUES (1,'DIURNA');
INSERT INTO `tipo_jornada` VALUES (2,'NOCTURNA');

CREATE TABLE `tipo_pago_prestamo` (
  `ID_TIPO_PAGO_PRESTAMO` int NOT NULL AUTO_INCREMENT,
  `DESCRIPCION` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID_TIPO_PAGO_PRESTAMO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `tipo_pago_prestamo` VALUES (1,'Quincenal'),(2,'Mensual');

CREATE TABLE `forma_pago` (
  `ID_TIPO_PAGO` int NOT NULL AUTO_INCREMENT,
  `DESCRIPCION` varchar(1000) NOT NULL,
  PRIMARY KEY (`ID_TIPO_PAGO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

INSERT INTO `forma_pago` VALUES (1,'MENSUAL');

CREATE TABLE `categoria` (
  `ID_CAT` int NOT NULL AUTO_INCREMENT,
  `COD_CAT` varchar(3) NOT NULL,
  `NOM_CAT` varchar(30) NOT NULL,
  `SAL_INI` decimal(17,2) NOT NULL COMMENT 'salario inicial',
  `SAL_FIN` decimal(17,2) NOT NULL COMMENT 'salario final',
  PRIMARY KEY (`ID_CAT`),
  UNIQUE KEY `COD_CATEGORIA_UNIQUE` (`COD_CAT`),
  UNIQUE KEY `NOM_CAT_UNIQUE` (`NOM_CAT`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;


CREATE TRIGGER `tr_auditar_categorias_campos` AFTER UPDATE ON `categoria` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.COD_CAT <=> NEW.COD_CAT) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Cod_cat'); END IF;
	IF NOT (OLD.NOM_CAT <=> NEW.NOM_CAT) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Nom_cat'); END IF;
	IF NOT (OLD.SAL_FIN <=> NEW.SAL_FIN) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Sal_fin'); END IF;
	IF NOT (OLD.SAL_INI <=> NEW.SAL_INI) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Sal_ini'); END IF;

   
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'CATEGORIAS',             
            CONCAT('Se modificaron los siguientes campos de la categoria (id ', NEW.id_cat, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `empleado` (
  `ID_TRB` int NOT NULL AUTO_INCREMENT,
  `COD_TRB` varchar(5) NOT NULL,
  `NOM_TRB` varchar(50) NOT NULL,
  `PUESTO_TRABAJO` varchar(100) DEFAULT NULL,
  `FEC_NAC` date NOT NULL,
  `IDEN_TRB` varchar(20) NOT NULL,
  `EST_TRB` varchar(1) NOT NULL,
  `TELEFONO` varchar(14) DEFAULT NULL,
  `CELULAR` varchar(15) DEFAULT NULL,
  `PASAPORTE` varchar(20) DEFAULT NULL,
  `RTN` varchar(15) DEFAULT NULL,
  `RESIDENCIA` varchar(20) DEFAULT NULL,
  `ANTECEDENTES` varchar(8) DEFAULT NULL,
  `IHS` varchar(13) DEFAULT NULL,
  `DIRECCION` varchar(40) NOT NULL,
  `FEC_DEF` date NOT NULL,
  `SEXO` varchar(1) NOT NULL,
  `SUELDO` decimal(17,2) NOT NULL,
  `AFECTA_IHS` varchar(1) NOT NULL,
  `AFECTA_FSV` varchar(1) NOT NULL,
  `AFECTA_IPP` varchar(1) DEFAULT NULL,
  `AFECTA_SIN` varchar(1) NOT NULL,
  `AFECTA_ISR` varchar(1) NOT NULL,
  `BANCOS` varchar(15) DEFAULT NULL,
  `NCUENTA` varchar(13) DEFAULT NULL,
  `COD_CUE` varchar(8) DEFAULT NULL,
  `ESTADO` varchar(1) DEFAULT NULL,
  `LICENCIA` varchar(15) DEFAULT NULL,
  `ID_DEP` int NOT NULL,
  `ID_CAT` int NOT NULL,
  `ID_TIPO_EMPLEADO` int NOT NULL,
  `ID_TIPO_PAGO` int NOT NULL,
  `TIPO_EMPLEADO` varchar(1) NOT NULL,
  `FECHA_CONTRATACION` date NOT NULL,
  `FECHA_INICIO` date NOT NULL,
  `CUENTA_SUELDO` decimal(17,2) DEFAULT NULL,
  `CUENTA_SEGURO_SOCIAL` decimal(17,2) DEFAULT NULL,
  `CUENTA_REGIMEN_ESPECIAL` decimal(17,2) DEFAULT NULL,
  `CUENTA_ISR` decimal(17,2) DEFAULT NULL,
  `OTRA_CUENTA_1` decimal(17,2) DEFAULT NULL,
  `OTRA_CUENTA_2` decimal(17,2) DEFAULT NULL,
  PRIMARY KEY (`ID_TRB`),
  UNIQUE KEY `EMPLEADO_UNIQUE` (`COD_TRB`),
  UNIQUE KEY `uq_empleado_dni` (`IDEN_TRB`),
  UNIQUE KEY `uq_empleado_rtn` (`RTN`),
  KEY `fk_empleado_tipo_empleado` (`ID_TIPO_EMPLEADO`),
  KEY `fk_empleado_departamento` (`ID_DEP`),
  KEY `fk_empleado_f_pago_id_forma_pago_idx` (`ID_TIPO_PAGO`),
  KEY `fk_empleado_categoria` (`ID_CAT`),
  CONSTRAINT `fk_empleado_categoria` FOREIGN KEY (`ID_CAT`) REFERENCES `categoria` (`ID_CAT`),
  CONSTRAINT `fk_empleado_depto_id_depto` FOREIGN KEY (`ID_DEP`) REFERENCES `departamento` (`ID_DEP`),
  CONSTRAINT `fk_empleado_forma_pago_id` FOREIGN KEY (`ID_TIPO_PAGO`) REFERENCES `forma_pago` (`ID_TIPO_PAGO`),
  CONSTRAINT `fk_empleado_tipo_empleado` FOREIGN KEY (`ID_TIPO_EMPLEADO`) REFERENCES `tipo_empleado` (`ID_TIPO_EMPLEADO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;


CREATE TRIGGER `tr_auditar_empleado_campos` AFTER UPDATE ON `empleado` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.AFECTA_FSV <=> NEW.AFECTA_FSV) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Afecta_fsv'); END IF;
	IF NOT (OLD.AFECTA_IHS <=> NEW.AFECTA_IHS) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Afecta_ihs'); END IF;
	IF NOT (OLD.AFECTA_IPP <=> NEW.AFECTA_IPP) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Afecta_ipp'); END IF;
	IF NOT (OLD.AFECTA_ISR <=> NEW.AFECTA_ISR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Afecta_isr'); END IF;
	IF NOT (OLD.AFECTA_SIN <=> NEW.AFECTA_SIN) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Afecta_sin'); END IF;
	IF NOT (OLD.ANTECEDENTES <=> NEW.ANTECEDENTES) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Antecedentes'); END IF;
	IF NOT (OLD.BANCOS <=> NEW.BANCOS) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Bancos'); END IF;
	IF NOT (OLD.CELULAR <=> NEW.CELULAR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Celular'); END IF;
	IF NOT (OLD.COD_CUE <=> NEW.COD_CUE) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cod_cue'); END IF;
	IF NOT (OLD.COD_TRB <=> NEW.COD_TRB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cod_trb'); END IF;
	IF NOT (OLD.CUENTA_ISR <=> NEW.CUENTA_ISR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cuenta_isr'); END IF;
	IF NOT (OLD.CUENTA_REGIMEN_ESPECIAL <=> NEW.CUENTA_REGIMEN_ESPECIAL) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cuenta_regimen_especial'); END IF;
	IF NOT (OLD.CUENTA_SEGURO_SOCIAL <=> NEW.CUENTA_SEGURO_SOCIAL) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cuenta_seguro_social'); END IF;
	IF NOT (OLD.CUENTA_SUELDO <=> NEW.CUENTA_SUELDO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cuenta_sueldo'); END IF;
	IF NOT (OLD.DIRECCION <=> NEW.DIRECCION) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Direccion'); END IF;
	IF NOT (OLD.EST_TRB <=> NEW.EST_TRB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Est_trb'); END IF;
	IF NOT (OLD.ESTADO <=> NEW.ESTADO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Estado'); END IF;
	IF NOT (OLD.FEC_DEF <=> NEW.FEC_DEF) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fec_def'); END IF;
	IF NOT (OLD.FEC_NAC <=> NEW.FEC_NAC) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fec_nac'); END IF;
	IF NOT (OLD.FECHA_CONTRATACION <=> NEW.FECHA_CONTRATACION) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fecha_contratacion'); END IF;
	IF NOT (OLD.FECHA_INICIO <=> NEW.FECHA_INICIO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fecha_inicio'); END IF;
	IF NOT (OLD.ID_CAT <=> NEW.ID_CAT) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_cat'); END IF;
	IF NOT (OLD.ID_DEP <=> NEW.ID_DEP) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_dep'); END IF;
	IF NOT (OLD.ID_TIPO_EMPLEADO <=> NEW.ID_TIPO_EMPLEADO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_empleado'); END IF;
	IF NOT (OLD.ID_TIPO_PAGO <=> NEW.ID_TIPO_PAGO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_pago'); END IF;
	IF NOT (OLD.IDEN_TRB <=> NEW.IDEN_TRB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Iden_trb'); END IF;
	IF NOT (OLD.IHS <=> NEW.IHS) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Ihs'); END IF;
	IF NOT (OLD.LICENCIA <=> NEW.LICENCIA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Licencia'); END IF;
	IF NOT (OLD.NCUENTA <=> NEW.NCUENTA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Ncuenta'); END IF;
	IF NOT (OLD.NOM_TRB <=> NEW.NOM_TRB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Nom_trb'); END IF;
	IF NOT (OLD.OTRA_CUENTA_1 <=> NEW.OTRA_CUENTA_1) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Otra_cuenta_1'); END IF;
	IF NOT (OLD.OTRA_CUENTA_2 <=> NEW.OTRA_CUENTA_2) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Otra_cuenta_2'); END IF;
	IF NOT (OLD.PASAPORTE <=> NEW.PASAPORTE) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Pasaporte'); END IF;
	IF NOT (OLD.PUESTO_TRABAJO <=> NEW.PUESTO_TRABAJO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Puesto_trabajo'); END IF;
	IF NOT (OLD.RESIDENCIA <=> NEW.RESIDENCIA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Residencia'); END IF;
	IF NOT (OLD.RTN <=> NEW.RTN) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rtn'); END IF;
	IF NOT (OLD.SEXO <=> NEW.SEXO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Sexo'); END IF;
	IF NOT (OLD.SUELDO <=> NEW.SUELDO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Sueldo'); END IF;
	IF NOT (OLD.TELEFONO <=> NEW.TELEFONO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Telefono'); END IF;
	IF NOT (OLD.TIPO_EMPLEADO <=> NEW.TIPO_EMPLEADO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Tipo_empleado'); END IF;

    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'EMPLEADO',             
            CONCAT('Se modificaron los siguientes campos el empleado (código ', NEW.cod_trb, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `departamento` (
  `ID_DEP` int NOT NULL AUTO_INCREMENT,
  `COD_DEP` varchar(3) NOT NULL,
  `NOM_DEP` varchar(30) NOT NULL,
  `ID_EMPLEADO` int DEFAULT NULL COMMENT 'nombre encargado',
  `ID_CUENTA` int DEFAULT NULL COMMENT 'codigo cuenta',
  PRIMARY KEY (`ID_DEP`),
  UNIQUE KEY `COD_DEP_UNIQUE` (`COD_DEP`),
  UNIQUE KEY `NOM_DEP_UNIQUE` (`NOM_DEP`),
  KEY `fk_empleado_id_encargado_jefe_idx` (`ID_EMPLEADO`),
  CONSTRAINT `fk_departamento` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TRIGGER `tr_auditar_departamentos_campos` AFTER UPDATE ON `departamento` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.COD_DEP <=> NEW.COD_DEP) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cod_dep'); END IF;
	IF NOT (OLD.ID_CUENTA <=> NEW.ID_CUENTA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_cuenta'); END IF;
	IF NOT (OLD.ID_EMPLEADO <=> NEW.ID_EMPLEADO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_empleado'); END IF;
	IF NOT (OLD.NOM_DEP <=> NEW.NOM_DEP) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Nom_dep'); END IF;

    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'CATEGORIAS',             
            CONCAT('Se modificaron los siguientes campos del departamento(id ', NEW.id_dep, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `antecedentes` (
  `ID_ANTECEDENTE` int NOT NULL AUTO_INCREMENT,
  `NUMERO_ANTECEDENTE` int NOT NULL,
  `FECHA_EMISION` date NOT NULL,
  `FECHA_VENCIMIENTO` date NOT NULL,
  `VIGENCIA` date NOT NULL,
  `LUGAR_ORIGEN` varchar(100) NOT NULL,
  `ID_EMPLEADO` int NOT NULL,
  `TIPO_ANTECEDENTE` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`ID_ANTECEDENTE`),
  UNIQUE KEY `NUMERO_ANTECENDENTE` (`NUMERO_ANTECEDENTE`),
  KEY `FK_ANTECENDENTE_EMPLEADO_idx` (`ID_EMPLEADO`),
  CONSTRAINT `FK_ANTECENDENTE_EMPLEADO` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `aumentos` (
  `AUMENTOS_ID` int NOT NULL AUTO_INCREMENT,
  `ID_EMPLEADO` int NOT NULL,
  `ID_CATEGORIA` int NOT NULL,
  `FECHA` datetime NOT NULL,
  `SUELDO_ANTERIOR` decimal(17,2) NOT NULL,
  `SUELDO_ACTUAL` decimal(17,2) NOT NULL,
  `TIPO_AUMENTO_ID` int NOT NULL,
  `PORCENTAJE` decimal(13,3) DEFAULT NULL,
  `MONTO` decimal(17,2) DEFAULT NULL,
  `TOTAL_MONTO` decimal(17,2) NOT NULL,
  `DESCRIPCION` varchar(30) NOT NULL,
  PRIMARY KEY (`AUMENTOS_ID`),
  KEY `FK_AUMENTOS_CATEGORIA` (`ID_CATEGORIA`),
  KEY `FK_AUMENTOS_EMPLEADO` (`ID_EMPLEADO`),
  KEY `FK_AUMENTOS_TIPO_AUMENTO_idx` (`TIPO_AUMENTO_ID`),
  CONSTRAINT `FK_AUMENTOS_CATEGORIA` FOREIGN KEY (`ID_CATEGORIA`) REFERENCES `categoria` (`ID_CAT`),
  CONSTRAINT `FK_AUMENTOS_EMPLEADO` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`),
  CONSTRAINT `FK_AUMENTOS_TIPO_AUMENTO` FOREIGN KEY (`TIPO_AUMENTO_ID`) REFERENCES `tipo_aumento` (`TIPO_AUMENTO_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;


CREATE TRIGGER `tr_auditar_aaumentos_campos` AFTER UPDATE ON `aumentos` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.DESCRIPCION <=> NEW.DESCRIPCION) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Descripcion'); END IF;
	IF NOT (OLD.FECHA <=> NEW.FECHA) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Fecha'); END IF;
	IF NOT (OLD.ID_CATEGORIA <=> NEW.ID_CATEGORIA) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Id_categoria'); END IF;
	IF NOT (OLD.MONTO <=> NEW.MONTO) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Monto'); END IF;
	IF NOT (OLD.PORCENTAJE <=> NEW.PORCENTAJE) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Porcentaje'); END IF;
	IF NOT (OLD.SUELDO_ACTUAL <=> NEW.SUELDO_ACTUAL) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Sueldo_actual'); END IF;
	IF NOT (OLD.SUELDO_ANTERIOR <=> NEW.SUELDO_ANTERIOR) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Sueldo_anterior'); END IF;
	IF NOT (OLD.TIPO_AUMENTO_ID <=> NEW.TIPO_AUMENTO_ID) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Tipo_aumento_id'); END IF;
	IF NOT (OLD.TOTAL_MONTO <=> NEW.TOTAL_MONTO) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Total_monto'); END IF;


    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro,
            cod_empleado
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'AUMENTOS',             
            CONCAT('Se modificaron los siguientes campos del aumento (id ', NEW.aumentos_id, '): ', v_campos_cambiados),             
            NOW()
            ;     
    END IF; 
END;

CREATE TABLE `ausencias` (
  `AUSENCIAS_ID` int NOT NULL AUTO_INCREMENT,
  `ID_EMPLEADO` int NOT NULL,
  `FECHA_INICIAL` date NOT NULL COMMENT 'FECHA DE INICIO DE AUSENCIA',
  `FECHA_FINAL` date NOT NULL COMMENT 'FECHA DE FINALIZACIÓN DE AUSENCIA',
  `NUMERO_DIAS_TRABAJADOS` int NOT NULL COMMENT 'NUMERO DE DIAS NO TRABAJADOS',
  `MONTO` decimal(17,2) NOT NULL COMMENT 'MONTO',
  `TIPO_AUSENCIA` int DEFAULT NULL,
  `ID_NOMINA` int NOT NULL COMMENT 'CODIGO NOMINA',
  `ID_TIPO_NOM` int DEFAULT NULL,
  `SEPTIMO` varchar(1) DEFAULT NULL,
  `ID_TIPO_AUSENCIA` int NOT NULL,
  PRIMARY KEY (`AUSENCIAS_ID`),
  KEY `FK_AUSENCIAS_EMPLEADO` (`ID_EMPLEADO`),
  KEY `FK_AUSENCIAS_TIPO_AUSENCIA_idx` (`ID_TIPO_AUSENCIA`),
  CONSTRAINT `FK_AUSENCIAS_EMPLEADO` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`),
  CONSTRAINT `FK_AUSENCIAS_TIPO_AUSENCIA` FOREIGN KEY (`ID_TIPO_AUSENCIA`) REFERENCES `tipo_ausencia` (`id_tipo_ausencia`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

 CREATE TRIGGER `tr_auditar_ausencias_campos` AFTER UPDATE ON `ausencias` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.FECHA_FINAL <=> NEW.FECHA_FINAL) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Fecha_final'); END IF;
	IF NOT (OLD.FECHA_INICIAL <=> NEW.FECHA_INICIAL) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Fecha_inicial'); END IF;
	IF NOT (OLD.ID_EMPLEADO <=> NEW.ID_EMPLEADO) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Id_empleado'); END IF;
	IF NOT (OLD.ID_NOMINA <=> NEW.ID_NOMINA) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Id_nomina'); END IF;
	IF NOT (OLD.ID_TIPO_AUSENCIA <=> NEW.ID_TIPO_AUSENCIA) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Id_tipo_ausencia'); END IF;
	IF NOT (OLD.ID_TIPO_NOM <=> NEW.ID_TIPO_NOM) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Id_tipo_nom'); END IF;
	IF NOT (OLD.MONTO <=> NEW.MONTO) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Monto'); END IF;
	IF NOT (OLD.NUMERO_DIAS_TRABAJADOS <=> NEW.NUMERO_DIAS_TRABAJADOS) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Numero_dias_trabajados'); END IF;
	IF NOT (OLD.SEPTIMO <=> NEW.SEPTIMO) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Septimo'); END IF;
	IF NOT (OLD.TIPO_AUSENCIA <=> NEW.TIPO_AUSENCIA) THEN SET v_campos_cambiados = CONCAT_WS(',', v_campos_cambiados, 'Tipo_ausencia'); END IF;

    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'AUSENCIAS',             
            CONCAT('Se modificaron los siguientes campos de la ausencia (id ', NEW.ausencias_id, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `descuento` (
  `ID_DESCUENTO` int NOT NULL AUTO_INCREMENT,
  `COD_DEC` varchar(3) NOT NULL,
  `NOM_DEC` varchar(30) NOT NULL,
  `VAL_DEC` decimal(17,2) DEFAULT NULL COMMENT 'VALOR DEL DESCUENTO',
  `FAC_DEC` decimal(17,7) DEFAULT NULL,
  `ID_TIPO_JORNADA` int NOT NULL COMMENT 'TIPOD DE JORNADA',
  `ID_TIPO_PAGO` int NOT NULL COMMENT 'TIPO DE DESCUENTO',
  `ID_COD_CUE` int DEFAULT NULL,
  PRIMARY KEY (`ID_DESCUENTO`),
  UNIQUE KEY `codigo_unique` (`COD_DEC`),
  UNIQUE KEY `nombre_unique` (`NOM_DEC`),
  KEY `fk_descuento_tipo_jornada_idx` (`ID_TIPO_JORNADA`),
  KEY `descuento_ibfk_1` (`ID_TIPO_PAGO`),
  CONSTRAINT `descuento_ibfk_1` FOREIGN KEY (`ID_TIPO_PAGO`) REFERENCES `tipo_pago` (`ID_TIPO_PAGO`),
  CONSTRAINT `fk_descuento_tipo_jornada` FOREIGN KEY (`ID_TIPO_JORNADA`) REFERENCES `tipo_jornada` (`id_tipo_jornada`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TRIGGER `tr_auditar_descuento_campos` AFTER UPDATE ON `descuento` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.COD_DEC <=> NEW.COD_DEC) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cod_dec'); END IF;
	IF NOT (OLD.FAC_DEC <=> NEW.FAC_DEC) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fac_dec'); END IF;
	IF NOT (OLD.ID_COD_CUE <=> NEW.ID_COD_CUE) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_cod_cue'); END IF;
	IF NOT (OLD.ID_TIPO_JORNADA <=> NEW.ID_TIPO_JORNADA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_jornada'); END IF;
	IF NOT (OLD.ID_TIPO_PAGO <=> NEW.ID_TIPO_PAGO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_pago'); END IF;
	IF NOT (OLD.NOM_DEC <=> NEW.NOM_DEC) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Nom_dec'); END IF;
	IF NOT (OLD.VAL_DEC <=> NEW.VAL_DEC) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Val_dec'); END IF;

    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'DESCUENTO',             
            CONCAT('Se modificaron los siguientes campos del descuento(id ', NEW.cod_dec, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END ;
CREATE TABLE `error_log` (
  `error_id` int NOT NULL AUTO_INCREMENT,
  `mensaje` varchar(10000) DEFAULT NULL,
  `tabla` varchar(45) DEFAULT NULL,
  `nombre_columna` varchar(45) DEFAULT NULL,
  `codigo_error` varchar(45) DEFAULT NULL,
  `fecha_error` timestamp NULL DEFAULT NULL,
  `valor_campo` text,
  `accion` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`error_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `historial_aumento` (
  `ID_EMPLEADO` int NOT NULL,
  `ID_CAT` int NOT NULL,
  `FECHA` datetime NOT NULL,
  `SUELDO_ANTERIOR` decimal(10,0) NOT NULL,
  `SUELDO_ACTUAL` decimal(10,0) NOT NULL,
  `MONTO` decimal(10,0) NOT NULL,
  PRIMARY KEY (`ID_EMPLEADO`,`ID_CAT`,`FECHA`),
  UNIQUE KEY `historial_aumento_unique` (`ID_EMPLEADO`,`ID_CAT`,`FECHA`),
  KEY `fk_historial_aumento_categoria_idx` (`ID_CAT`),
  CONSTRAINT `fk_historial_aumento_categoria` FOREIGN KEY (`ID_CAT`) REFERENCES `categoria` (`ID_CAT`),
  CONSTRAINT `fk_historial_aumento_empleado` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `historial_sueldo` (
  `HISTORIAL_SUELDO_ID` int NOT NULL AUTO_INCREMENT,
  `ID_EMPLEADO` int NOT NULL,
  `PERIODO` int DEFAULT NULL,
  `ENERO` decimal(17,2) DEFAULT NULL,
  `FEBRERO` decimal(17,2) DEFAULT NULL,
  `MARZO` decimal(17,2) DEFAULT NULL,
  `ABRIL` decimal(17,2) DEFAULT NULL,
  `MAYO` decimal(17,2) DEFAULT NULL,
  `JUNIO` decimal(17,2) DEFAULT NULL,
  `JULIO` decimal(17,2) DEFAULT NULL,
  `AGOSTO` decimal(17,2) DEFAULT NULL,
  `SEPTIEMBRE` decimal(17,2) DEFAULT NULL,
  `OCTUBRE` decimal(17,2) DEFAULT NULL,
  `NOVIEMBRE` decimal(17,2) DEFAULT NULL,
  `DICIEMBRE` decimal(17,2) DEFAULT NULL,
  `TOTAL` decimal(17,2) DEFAULT NULL,
  `ISR_ENERO` decimal(17,2) DEFAULT NULL,
  `ISR_FEBRERO` decimal(17,2) DEFAULT NULL,
  `ISR_MARZO` decimal(17,2) DEFAULT NULL,
  `ISR_ABRIL` decimal(17,2) DEFAULT NULL,
  `ISR_MAYO` decimal(17,2) DEFAULT NULL,
  `ISR_JUNIO` decimal(17,2) DEFAULT NULL,
  `ISR_JULIO` decimal(17,2) DEFAULT NULL,
  `ISR_AGOSTO` decimal(17,2) DEFAULT NULL,
  `ISR_SEPTIEMRE` decimal(17,2) DEFAULT NULL,
  `ISR_OCTUBRE` decimal(17,2) DEFAULT NULL,
  `ISR_NOVIEMBRE` decimal(17,2) DEFAULT NULL,
  `ISR_DICIEMBRE` decimal(17,2) DEFAULT NULL,
  `ISR_ANUAL` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`HISTORIAL_SUELDO_ID`),
  UNIQUE KEY `HISTORIAL_SUELDO_UNIQUE` (`ID_EMPLEADO`,`PERIODO`),
  CONSTRAINT `historial_sueldo_empleado` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `labores` (
  `ID_LAB` int NOT NULL AUTO_INCREMENT,
  `COD_LAB` varchar(3) NOT NULL,
  `NOM_LAB` varchar(30) NOT NULL,
  `ID_TIPO_JORNADA` int NOT NULL COMMENT 'TIPO DE JORNADA: DIURNA, NOCTURNA',
  `VAL_LAB` decimal(17,2) DEFAULT '0.00' COMMENT 'VALOR DE LA LABOR, MONTO DE LA LABOR',
  `FAC_LAB` decimal(17,7) DEFAULT '0.0000000' COMMENT 'FACTOR DE LA LABOR DEPENDE DEL TIPO DE LABOR SI ES FACTOR EN TIPO DE LABOR SE ACTIVA ESTE CAMPO',
  `ID_TIPO_PAGO` int NOT NULL COMMENT 'TIPO DE LABOR:\nD : DEFINIDO POR EL USUARIO\nF: FACTOR\nH: HORA\nv: VALOR',
  `ID_CUENTA` int NOT NULL,
  PRIMARY KEY (`ID_LAB`),
  UNIQUE KEY `codigo_labor_unique` (`COD_LAB`),
  UNIQUE KEY `nombre_labor_unique` (`NOM_LAB`),
  KEY `fk_tipo_jornada_idx` (`ID_TIPO_JORNADA`),
  KEY `fk_labores_tipo_labor` (`ID_TIPO_PAGO`),
  CONSTRAINT `fk_labores_tipo_labor` FOREIGN KEY (`ID_TIPO_PAGO`) REFERENCES `tipo_pago` (`ID_TIPO_PAGO`),
  CONSTRAINT `fk_tipo_jornada` FOREIGN KEY (`ID_TIPO_JORNADA`) REFERENCES `tipo_jornada` (`id_tipo_jornada`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TRIGGER `tr_auditar_labores_campos` AFTER UPDATE ON `labores` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.COD_LAB <=> NEW.COD_LAB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cod_lab'); END IF;
	IF NOT (OLD.FAC_LAB <=> NEW.FAC_LAB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fac_lab'); END IF;
	IF NOT (OLD.ID_CUENTA <=> NEW.ID_CUENTA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_cuenta'); END IF;
	IF NOT (OLD.ID_TIPO_JORNADA <=> NEW.ID_TIPO_JORNADA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_jornada'); END IF;
	IF NOT (OLD.ID_TIPO_PAGO <=> NEW.ID_TIPO_PAGO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_pago'); END IF;
	IF NOT (OLD.NOM_LAB <=> NEW.NOM_LAB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Nom_lab'); END IF;
	IF NOT (OLD.VAL_LAB <=> NEW.VAL_LAB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Val_lab'); END IF;

    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'LABORES',             
            CONCAT('Se modificaron los siguientes campos la labor (código ', NEW.COD_LAB, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;
CREATE TABLE `mdescuentos` (
  `ID_MDESCUENTOS` int NOT NULL AUTO_INCREMENT,
  `ID_EMPLEADO` int NOT NULL,
  `ID_DESCUENTO` int NOT NULL,
  `DESCRIPCION_DESCUENTO` varchar(30) NOT NULL,
  `CANT_DESCUENTO` decimal(8,2) NOT NULL,
  `FECHA_DESCUENTO` date NOT NULL,
  `MON_DESCUENTO` decimal(17,2) NOT NULL,
  `ID_CUENTA` int NOT NULL,
  `ID_NOMINA` int DEFAULT NULL,
  `ID_TIPO_PAGO` int NOT NULL,
  PRIMARY KEY (`ID_MDESCUENTOS`),
  UNIQUE KEY `mdescuentos_unique` (`ID_EMPLEADO`,`ID_DESCUENTO`,`FECHA_DESCUENTO`),
  KEY `fk_mdescuentos_empleado_idx` (`ID_EMPLEADO`),
  KEY `fk_mdescuentos_descuentos_idx` (`ID_DESCUENTO`),
  KEY `fk_mdescuentos_tipopago_idx` (`ID_TIPO_PAGO`),
  CONSTRAINT `fk_mdescuentos_descuentos` FOREIGN KEY (`ID_DESCUENTO`) REFERENCES `descuento` (`ID_DESCUENTO`),
  CONSTRAINT `fk_mdescuentos_empleado` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`),
  CONSTRAINT `fk_mdescuentos_tipopago` FOREIGN KEY (`ID_TIPO_PAGO`) REFERENCES `tipo_pago` (`ID_TIPO_PAGO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TRIGGER `tr_auditar_mdescuentos_campos` AFTER UPDATE ON `mdescuentos` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.CANT_DESCUENTO <=> NEW.CANT_DESCUENTO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cant_descuento'); END IF;
	IF NOT (OLD.DESCRIPCION_DESCUENTO <=> NEW.DESCRIPCION_DESCUENTO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Descripcion_descuento'); END IF;
	IF NOT (OLD.FECHA_DESCUENTO <=> NEW.FECHA_DESCUENTO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fecha_descuento'); END IF;
	IF NOT (OLD.ID_CUENTA <=> NEW.ID_CUENTA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_cuenta'); END IF;
	IF NOT (OLD.ID_DESCUENTO <=> NEW.ID_DESCUENTO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_descuento'); END IF;
	IF NOT (OLD.ID_EMPLEADO <=> NEW.ID_EMPLEADO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_empleado'); END IF;
	IF NOT (OLD.ID_NOMINA <=> NEW.ID_NOMINA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_nomina'); END IF;
	IF NOT (OLD.ID_TIPO_PAGO <=> NEW.ID_TIPO_PAGO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_pago'); END IF;
	IF NOT (OLD.MON_DESCUENTO <=> NEW.MON_DESCUENTO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Mon_descuento'); END IF;

    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'MDESCUENTOS',             
            CONCAT('Se modificaron los siguientes campos el movimiento descuento (código ', NEW.id_mdescuentos, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `mlabores` (
  `ID_MLABORES` int NOT NULL AUTO_INCREMENT,
  `ID_EMPLEADO` int NOT NULL,
  `ID_LABOR` int NOT NULL,
  `DESCRIPCION_LAB` varchar(30) NOT NULL,
  `CANTIDAD_LAB` decimal(7,2) NOT NULL,
  `FECHA_LABOR` date NOT NULL,
  `MONTO_LABOR` decimal(17,2) NOT NULL,
  `ID_CUENTA` int DEFAULT NULL,
  `ID_NOMINA` int DEFAULT NULL,
  `ISR` varchar(1) NOT NULL,
  `ID_TIPO_PAGO` int NOT NULL,
  PRIMARY KEY (`ID_MLABORES`,`CANTIDAD_LAB`),
  UNIQUE KEY `mlabores` (`ID_EMPLEADO`,`ID_LABOR`,`FECHA_LABOR`),
  KEY `FK_MLABORES_TIPO_PAGO_idx` (`ID_TIPO_PAGO`),
  KEY `FK_MLABORES_LABORES` (`ID_LABOR`),
  CONSTRAINT `FK_MLABORES_EMPLEADO` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`),
  CONSTRAINT `FK_MLABORES_LABORES` FOREIGN KEY (`ID_LABOR`) REFERENCES `labores` (`ID_LAB`),
  CONSTRAINT `FK_MLABORES_TIPO_PAGO` FOREIGN KEY (`ID_TIPO_PAGO`) REFERENCES `tipo_pago` (`ID_TIPO_PAGO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TRIGGER `tr_auditar_mlabores_campos` AFTER UPDATE ON `mlabores` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.CANTIDAD_LAB <=> NEW.CANTIDAD_LAB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cantidad_lab'); END IF;
	IF NOT (OLD.DESCRIPCION_LAB <=> NEW.DESCRIPCION_LAB) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Descripcion_lab'); END IF;
	IF NOT (OLD.FECHA_LABOR <=> NEW.FECHA_LABOR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fecha_labor'); END IF;
	IF NOT (OLD.ID_CUENTA <=> NEW.ID_CUENTA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_cuenta'); END IF;
	IF NOT (OLD.ID_EMPLEADO <=> NEW.ID_EMPLEADO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_empleado'); END IF;
	IF NOT (OLD.ID_LABOR <=> NEW.ID_LABOR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_labor'); END IF;
	IF NOT (OLD.ID_NOMINA <=> NEW.ID_NOMINA) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_nomina'); END IF;
	IF NOT (OLD.ID_TIPO_PAGO <=> NEW.ID_TIPO_PAGO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_pago'); END IF;
	IF NOT (OLD.ISR <=> NEW.ISR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Isr'); END IF;
	IF NOT (OLD.MONTO_LABOR <=> NEW.MONTO_LABOR) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Monto_labor'); END IF;

    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'MLABORES',             
            CONCAT('Se modificaron los siguientes campos el movimiento labor (id ', NEW.id_mlabores, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `parametro` (
  `PARAMETRO_ID` int NOT NULL AUTO_INCREMENT,
  `PERIODO` int NOT NULL,
  `EXCENTO` decimal(50,2) NOT NULL,
  `RANGO_INICIAL15` decimal(50,2) NOT NULL,
  `RANGO_FINAL15` decimal(50,2) NOT NULL,
  `RANGO_INICIAL20` decimal(50,2) NOT NULL,
  `RANGO_FINAL20` decimal(50,2) NOT NULL,
  `RANGO_INICIAL25` decimal(50,2) NOT NULL,
  `RANGO_FINAL25` decimal(50,2) NOT NULL,
  `SUELDO_PROMEDIO` decimal(20,2) NOT NULL,
  `RESERVA_LAB_RAP` decimal(20,2) NOT NULL,
  `VALOR_PISO_RAP` decimal(20,2) NOT NULL,
  `VALOR_TECHO_IHSS` decimal(20,2) NOT NULL,
  `SALARIO_MINIMO_PROMEDIO` decimal(20,2) NOT NULL,
  `IPP` decimal(50,2) DEFAULT NULL,
  `SINDICATO` decimal(50,2) DEFAULT NULL,
  PRIMARY KEY (`PARAMETRO_ID`),
  UNIQUE KEY `PERIODO_UNIQUE` (`PERIODO`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

 CREATE TRIGGER `tr_auditar_parametro_campos` AFTER UPDATE ON `parametro` FOR EACH ROW BEGIN     
    DECLARE v_campos_cambiados TEXT DEFAULT '';  

	IF NOT (OLD.EXCENTO <=> NEW.EXCENTO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Excento'); END IF;
	IF NOT (OLD.PERIODO <=> NEW.PERIODO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Periodo'); END IF;
	IF NOT (OLD.RANGO_FINAL15 <=> NEW.RANGO_FINAL15) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rango_final15'); END IF;
	IF NOT (OLD.RANGO_FINAL20 <=> NEW.RANGO_FINAL20) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rango_final20'); END IF;
	IF NOT (OLD.RANGO_FINAL25 <=> NEW.RANGO_FINAL25) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rango_final25'); END IF;
	IF NOT (OLD.RANGO_INICIAL15 <=> NEW.RANGO_INICIAL15) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rango_inicial15'); END IF;
	IF NOT (OLD.RANGO_INICIAL20 <=> NEW.RANGO_INICIAL20) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rango_inicial20'); END IF;
	IF NOT (OLD.RANGO_INICIAL25 <=> NEW.RANGO_INICIAL25) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Rango_inicial25'); END IF;
	IF NOT (OLD.RESERVA_LAB_RAP <=> NEW.RESERVA_LAB_RAP) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Reserva_lab_rap'); END IF;
	IF NOT (OLD.SALARIO_MINIMO_PROMEDIO <=> NEW.SALARIO_MINIMO_PROMEDIO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Salario_minimo_promedio'); END IF;
	IF NOT (OLD.SUELDO_PROMEDIO <=> NEW.SUELDO_PROMEDIO) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Sueldo_promedio'); END IF;
	IF NOT (OLD.VALOR_PISO_RAP <=> NEW.VALOR_PISO_RAP) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Valor_piso_rap'); END IF;
	IF NOT (OLD.VALOR_TECHO_IHSS <=> NEW.VALOR_TECHO_IHSS) THEN SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Valor_techo_ihss'); END IF;

    -- 3. Si hubo cambios, registramos una sola fila en la bitácora     
    IF v_campos_cambiados <> '' THEN         
        INSERT INTO bitacora (             
            nombre_usuario,             
            accion,             
            tabla_afectada,             
            descripcion,             
            fecha_registro         
        )         
        SELECT              
            @usuario_actual ,         
            'MODIFICAR',             
            'PARAMETRO',             
            CONCAT('Se modificaron los siguientes campos el parametro con (periodo ', NEW.PERIODO, '): ', v_campos_cambiados),             
            NOW();     
    END IF; 
END;

CREATE TABLE `planilla` (
  `ID_PLANILLA` int NOT NULL AUTO_INCREMENT,
  `COD_PLANILLA` varchar(10) NOT NULL,
  `FECHA` date NOT NULL,
  `ID_EMPLEADO` int NOT NULL,
  `SUELDO` decimal(17,2) DEFAULT NULL,
  `DIARIO` decimal(17,2) DEFAULT NULL,
  `LABORES` decimal(17,2) DEFAULT NULL,
  `AUMENTO` decimal(17,2) DEFAULT NULL,
  `SALARIO` decimal(17,2) DEFAULT NULL,
  `IHSS` decimal(17,2) DEFAULT NULL,
  `RAP` decimal(17,2) DEFAULT NULL,
  `ISR` decimal(17,2) DEFAULT NULL,
  `AUSENCIAS` decimal(17,2) DEFAULT NULL,
  `SEPTIMO` decimal(17,2) DEFAULT NULL,
  `DEDUCCIONES` decimal(17,2) DEFAULT NULL,
  `PRESTAMO` decimal(17,2) DEFAULT NULL,
  `DESCUENTOS` decimal(17,2) DEFAULT NULL,
  `SALARIO_NETO` decimal(17,2) DEFAULT NULL,
  PRIMARY KEY (`ID_PLANILLA`),
  UNIQUE KEY `PRESTAMOS_UNIQUE` (`COD_PLANILLA`,`FECHA`,`ID_EMPLEADO`),
  KEY `FK_PLANILLA_EMPLEADO` (`ID_EMPLEADO`),
  CONSTRAINT `FK_PLANILLA_EMPLEADO` FOREIGN KEY (`ID_EMPLEADO`) REFERENCES `empleado` (`ID_TRB`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `prestamo` (
  `ID_PRESTAMO` int NOT NULL AUTO_INCREMENT,
  `CODIGO` varchar(5) NOT NULL,
  `ID_EMPLEADO` int NOT NULL,
  `FECHA` date NOT NULL,
  `DESCRIPCION` varchar(40) DEFAULT NULL,
  `MONTO` decimal(17,2) NOT NULL,
  `ESTADO` varchar(1) NOT NULL,
  `CUOTA` decimal(17,2) DEFAULT NULL,
  `CUOTA_MES` decimal(17,2) DEFAULT NULL,
  `ID_TIPO_PAGO` int NOT NULL,
  `TIEMPO` int NOT NULL,
  `P_ANT` decimal(17,2) DEFAULT NULL,
  `P_DEB` decimal(17,2) DEFAULT NULL,
  `P_CRED` decimal(17,2) DEFAULT NULL,
  `P_ACT` decimal(17,2) DEFAULT NULL,
  `COD_CPRES` varchar(8) DEFAULT NULL,
  `ACCESO` varchar(24) DEFAULT NULL,
  PRIMARY KEY (`ID_PRESTAMO`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4;

CREATE TRIGGER `tr_auditar_prestamo_campos` 
AFTER UPDATE ON `prestamo` 
FOR EACH ROW 
BEGIN 
    DECLARE v_campos_cambiados TEXT DEFAULT ''; 
    
    IF NOT (OLD.ACCESO <=> NEW.ACCESO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Acceso'); 
    END IF; 
    IF NOT (OLD.CODIGO <=> NEW.CODIGO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Codigo'); 
    END IF; 
    IF NOT (OLD.COD_CPRES <=> NEW.COD_CPRES) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cod_cpres'); 
    END IF; 
    IF NOT (OLD.CUOTA <=> NEW.CUOTA) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cuota'); 
    END IF; 
    IF NOT (OLD.CUOTA_MES <=> NEW.CUOTA_MES) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Cuota_mes'); 
    END IF; 
    IF NOT (OLD.DESCRIPCION <=> NEW.DESCRIPCION) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Descripcion'); 
    END IF; 
    IF NOT (OLD.ESTADO <=> NEW.ESTADO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Estado'); 
    END IF; 
    IF NOT (OLD.FECHA <=> NEW.FECHA) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Fecha'); 
    END IF; 
    IF NOT (OLD.ID_EMPLEADO <=> NEW.ID_EMPLEADO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_empleado'); 
    END IF; 
    IF NOT (OLD.ID_TIPO_PAGO <=> NEW.ID_TIPO_PAGO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Id_tipo_pago'); 
    END IF; 
    IF NOT (OLD.MONTO <=> NEW.MONTO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Monto'); 
    END IF; 
    IF NOT (OLD.P_ACT <=> NEW.P_ACT) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'P_act'); 
    END IF; 
    IF NOT (OLD.P_ANT <=> NEW.P_ANT) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'P_ant'); 
    END IF; 
    IF NOT (OLD.P_CRED <=> NEW.P_CRED) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'P_cred'); 
    END IF; 
    IF NOT (OLD.P_DEB <=> NEW.P_DEB) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'P_deb'); 
    END IF; 
    IF NOT (OLD.TIEMPO <=> NEW.TIEMPO) THEN 
        SET v_campos_cambiados = CONCAT_WS(', ', v_campos_cambiados, 'Tiempo'); 
    END IF; 

    IF v_campos_cambiados <> '' THEN 
        INSERT INTO bitacora (
            nombre_usuario, 
            accion, 
            tabla_afectada, 
            descripcion, 
            fecha_registro
        ) 
        SELECT 
            @usuario_actual, 
            'MODIFICAR', 
            'PRESTAMO', 
            CONCAT('Se modificaron los siguientes campos del prestamo con (id ', NEW.ID_PRESTAMO, '): ', v_campos_cambiados), 
            NOW(); 
    END IF; 
END;

-- Volvemos a encender las llaves foráneas para mantener la integridad
SET FOREIGN_KEY_CHECKS = 1;