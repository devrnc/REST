CREATE TABLE `payments` (
	`Id` INT(10) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
	`Name` VARCHAR(50) NULL NULL NULL NULL,
	`DueDate` DATE NULL NULL NULL NULL,
	`Payday` DATE NULL NULL NULL NULL,
	`Value` DECIMAL NULL NULL NULL NULL NULL NULL,
	`CorrectedValue` DECIMAL NULL NULL NULL NULL NULL NULL,
	`Days` INT(5) UNSIGNED NULL NULL NULL NULL NULL NULL
)
ENGINE=InnoDB
;
