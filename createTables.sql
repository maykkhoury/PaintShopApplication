CREATE TABLE language(
    id_language LONG NOT NULL,
    code_language CHAR(9) NOT NULL,
    label_language CHAR(50) NOT NULL,
    isDefault CHAR(1) NOT NULL,
    dateTimeFormat CHAR(50) NOT NULL, -- DEFAULT 'MM/dd/yy H:mm:ss' 
    PRIMARY KEY (id_language),
    UNIQUE (code_language)    
);

CREATE TABLE usercurrency(
    id_currency LONG NOT NULL,
    code_currency CHAR(9) NOT NULL,
    symbol CHAR(5) NOT NULL,
    rateToDollar DOUBLE NOT NULL,
    PRIMARY KEY (id_currency),
    UNIQUE (code_currency)    
);

CREATE TABLE currencyLanguage(
    id_currencyLanguage LONG NOT NULL,
    id_currency LONG NOT NULL,
    id_language LONG NOT NULL,
    label CHAR(50),
    PRIMARY KEY (id_currencyLanguage)   
);

ALTER TABLE currencyLanguage ADD 
   CONSTRAINT unCuLa
      UNIQUE (id_currency, id_language);


CREATE TABLE unit(
    id_unit LONG NOT NULL,
    rateToLitre DOUBLE NOT NULL,
    code_unit CHAR(5) NOT NULL,
    PRIMARY KEY (id_unit)
);

CREATE TABLE unitLanguage(
    id_unitLanguage LONG NOT NULL,
    id_unit LONG NOT NULL,
    id_language LONG NOT NULL,
    name_unit CHAR(50),
    PRIMARY KEY (id_unitLanguage)   
);
ALTER TABLE unitLanguage ADD 
   CONSTRAINT unUnLa
      UNIQUE (id_unit,id_language) ;


CREATE TABLE garage(
    id_garage LONG NOT NULL,
    id_language LONG,
    id_responsible LONG,
    chosen CHAR(1),
    PRIMARY KEY (id_garage)
);

CREATE TABLE garageLanguage(
    id_garageLanguage LONG NOT NULL,
    id_garage LONG NOT NULL,
    id_language LONG NOT NULL,
    garageName CHAR(50),
    location CHAR(100),
    description CHAR(100),
    logo CHAR(90),
    PRIMARY KEY (id_garageLanguage) 
);
ALTER TABLE garageLanguage ADD 
   CONSTRAINT unGaLa
      UNIQUE (id_garage,id_language) ;



CREATE TABLE color(
    id_color LONG NOT NULL,
    colorImgPath CHAR(100),
    defaultPrice DOUBLE,
    id_defaultCurreny LONG,
    id_defaultUnit LONG,
    name_color CHAR(100)
    PRIMARY KEY (id_color)
);


CREATE TABLE formulaColor(
    id_formulaColor LONG NOT NULL,
    id_formula LONG NOT NULL,
    id_color LONG NOT NULL,
    quantite DOUBLE NOT NULL,
    id_Unit LONG NOT NULL,
    PRIMARY KEY (id_formulaColor)
);
ALTER TABLE formulaColor ADD 
   CONSTRAINT unFoCo
      UNIQUE (id_color,id_formula);



CREATE TABLE formula(
    id_formula LONG NOT NULL,
    id_car LONG NOT NULL,
    name_formula CHAR(50) NOT NULL,
    type CHAR(50),
    version CHAR(5),
    formulaImgPath CHAR(100),
    otherPrice DOUBLE,
    id_otherCurreny LONG,
    id_otherUnit LONG,
    PRIMARY KEY (id_formula)
);


CREATE TABLE transactionTable(
    id_transaction LONG NOT NULL,
    transactionDate DATE NOT NULL,
    discount DOUBLE,
    amount DOUBLE,
    id_curreny LONG,
    PRIMARY KEY (id_transaction)
);


CREATE TABLE transactionFormula(
    id_transactionFormula LONG NOT NULL,
    id_formula LONG NOT NULL,
    id_transaction LONG NOT NULL,
    PRIMARY KEY (id_transactionFormula)
);
ALTER TABLE transactionFormula ADD 
   CONSTRAINT unTrFo
      UNIQUE (id_transaction ,id_formula);


CREATE TABLE label(
    id_label LONG NOT NULL,
    name_label char(10) NOT NULL,
    PRIMARY KEY (id_label)
);

CREATE TABLE labelLanguage(
    id_labelLanguage LONG NOT NULL,
    id_label LONG NOT NULL,
    id_language LONG NOT NULL,
    description CHAR(100),
    PRIMARY KEY (id_labelLanguage)   
);
ALTER TABLE labelLanguage ADD 
   CONSTRAINT unlaLa
      UNIQUE (id_label,id_language) ;
      
CREATE TABLE car(
    id_car LONG NOT NULL,
    carImgPath CHAR(100),
    model CHAR(50),
    carName CHAR(50),
    PRIMARY KEY (id_car)
);