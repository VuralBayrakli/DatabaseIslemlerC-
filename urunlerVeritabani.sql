CREATE SCHEMA `urunler` ;
CREATE TABLE `urunler`.`urunler` (
  `urunid` INT NOT NULL AUTO_INCREMENT,
  `urunadi` VARCHAR(45) NULL,
  `urunbirimi` VARCHAR(45) NULL,
  `urunfiyati` VARCHAR(45) NULL,
  `urunkategori` VARCHAR(45) NULL,
  PRIMARY KEY (`urunid`));