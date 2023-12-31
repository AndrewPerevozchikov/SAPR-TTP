USE [PCTEXT]
GO
/****** Object:  StoredProcedure [dbo].[e012p147_GetTipStandartOperation]    Script Date: 06.10.2023 14:19:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Процедура получение операций из дерева стандартных ТТП по типу
-- Автор: Перевозчиков Андрей Иванович 556176
-- Дата создания: 14.09.2023 
-- Входные параметры: 
--@nom_mas  - номер массива,
--@Level – уровень пункта дерева
--@Tip – тип переменной (пункт дерева)
--@id_operation – идентификатор  операции 
--@number_trek – номер перехода (0- все, иначе конкретный)
--@pornom – порядковый номер (0- все, иначе конкретный)

-- =============================================
ALTER       PROCEDURE [dbo].[e012p147_GetTipStandartOperation]
	@NOM_MAS int,
	@LEVEL smallint,
	@TIP smallint,
	@id_operation uniqueidentifier,
	@number_trek char(3),
	@PORNOM smallint
AS
BEGIN
	/*	declare
	@NOM_MAS int,
	@LEVEL smallint,
	@TIP smallint,
	@id_operation uniqueidentifier,
	@number_trek char(3),
	@PORNOM smallint

SET @NOM_MAS = 5
SET @LEVEL = 3
SET @TIP = 17
SET @id_operation = '9415A9EB-ABDB-409C-808A-43DEDC0B4D87'
--SET @id_operation = '81817B09-5088-4218-931D-19B2DA24788E'
--SET @id_operation = '7DBC558E-0080-486F-B832-D53366D87549'
SET @id_operation = '2B9AA5B8-2C56-4295-8572-BC26002023AE'
SET @number_trek = 0
SET @PORNOM = 2*/

--tip=10 ОПЕРАЦИИ [A]
if(@TIP=10)
BEGIN
select * from  operation
WHERE NOM_MAS = @NOM_MAS And id_operation = @id_operation 
END

--tip=13 ОБОРУДОВАНИЕ [Б]
if(@TIP=13)
BEGIN
if @PORNOM = 0 --все, иначе конкретный
     select * from  eqipment
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation
	  order by pornom
   else
     select * from  eqipment
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation AND pornom = @PORNOM 
END

--tip=17 МАТЕРИАЛЫ [М]
if(@TIP=17)
BEGIN
if @PORNOM = 0 --все, иначе конкретный
     select *,NN = dse from  material b
	left join NSB..GPF  b1 on b1.id_gpf=b.id_nn_material 
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation
	  order by pornom
   else
     select *,NN = dse from  material b
	left join NSB..GPF  b1 on b1.id_gpf=b.id_nn_material  
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation AND pornom = @PORNOM 
END

--tip 19 Оснастка 
if(@TIP=19)
 begin
  if @level=3 --к операции, иначе к переходам
   begin
	 if(@PORNOM = 0)
	   select * ,pr_name=(case when ipr=1 then 'ПРИСПОСОБЛЕНИЯ'
									  when ipr=2 then 'ВСПОМОГАТЕЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=3 then 'РЕЖУЩИЙ ИНСТРУМЕНТ'
									  when ipr=4 then 'СЛЕСАРНО-МОНТАЖНЫЙ ИНСТРУМЕНТ'
									  when ipr=5 then 'СПЕЦИАЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=6 then 'МЕРИТЕЛЬНЫЙ ИНСТРУМЕНТ'
					                  else 'ПРИСПОСОБЛЕНИЯ' end) 
	   from  PROP
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
		order by pornom
	 else
	   select *,pr_name=(case when ipr=1 then 'ПРИСПОСОБЛЕНИЯ'
									  when ipr=2 then 'ВСПОМОГАТЕЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=3 then 'РЕЖУЩИЙ ИНСТРУМЕНТ'
									  when ipr=4 then 'СЛЕСАРНО-МОНТАЖНЫЙ ИНСТРУМЕНТ'
									  when ipr=5 then 'СПЕЦИАЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=6 then 'МЕРИТЕЛЬНЫЙ ИНСТРУМЕНТ'
					                  else 'ПРИСПОСОБЛЕНИЯ' end) 
		from  PROP
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  AND pornom=@PORNOM 
		and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
   end
  else
   begin
	 if(@PORNOM = 0)
	   select * ,pr_name=(case when ipr=1 then 'ПРИСПОСОБЛЕНИЯ'
									  when ipr=2 then 'ВСПОМОГАТЕЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=3 then 'РЕЖУЩИЙ ИНСТРУМЕНТ'
									  when ipr=4 then 'СЛЕСАРНО-МОНТАЖНЫЙ ИНСТРУМЕНТ'
									  when ipr=5 then 'СПЕЦИАЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=6 then 'МЕРИТЕЛЬНЫЙ ИНСТРУМЕНТ'
					                  else 'ПРИСПОСОБЛЕНИЯ' end) 
	   from  PROP
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) =@number_trek
		order by pornom
	 else
	   select * ,pr_name=(case when ipr=1 then 'ПРИСПОСОБЛЕНИЯ'
									  when ipr=2 then 'ВСПОМОГАТЕЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=3 then 'РЕЖУЩИЙ ИНСТРУМЕНТ'
									  when ipr=4 then 'СЛЕСАРНО-МОНТАЖНЫЙ ИНСТРУМЕНТ'
									  when ipr=5 then 'СПЕЦИАЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=6 then 'МЕРИТЕЛЬНЫЙ ИНСТРУМЕНТ'
					                  else 'ПРИСПОСОБЛЕНИЯ' end) 
	   from  PROP
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) = @number_trek 
		AND pornom=@PORNOM 
   end
 end

 --tip 21 РЕЖИМЫ [Р] 
if(@TIP=21)
 begin
  if @level=3 --к операции, иначе к переходам
   begin
	 if(@PORNOM = 0)
	   select * from  mode
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
		order by pornom
	 else
	   select * from  mode
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation AND pornom=@PORNOM 
		and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
   end
  else
   begin
	 if(@PORNOM = 0)
	   select * from  mode
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  and Ltrim(rtrim(number_trek)) =@number_trek
		order by pornom
	 else
	   select * from  mode
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  and Ltrim(rtrim(number_trek)) = @number_trek 
		AND pornom=@PORNOM 
   end
 end

 --tip=24 ПЕРЕХОДЫ [О]
if(@TIP=24)
BEGIN
if @number_trek = 0 --все, иначе конкретный
     select * from  trek
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation
	  order by number_trek
   else
     select * from  trek
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation AND Ltrim(rtrim(number_trek)) = @number_trek 
END

--tip=26 ВНИМАНИЕ [В] 
if(@TIP=26)
BEGIN
if @PORNOM = 0 --все, иначе конкретный
     select * from  operationFT
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation and ST='В'
	  order by pornom
   else
     select * from  operationFT
      WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation AND pornom = @PORNOM and ST='В'
END

 --tip 27 УКАЗАНИЕ [У]  
if(@TIP=27)
 begin
  if @level=3 --к операции, иначе к переходам
   begin
	 if(@PORNOM = 0)
	   select * from  operationFT
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  and ST='У'

		order by pornom
	 else
	   select * from  operationFT
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation  AND pornom=@PORNOM 
		and ST='У'
   end
  else
   begin
	 if(@PORNOM = 0)
	   select * from  trekFT
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation   and Ltrim(rtrim(number_trek)) =@number_trek and ST='У'
		order by pornom
	 else
	   select * from  trekFT
		WHERE  NOM_MAS = @NOM_MAS And id_operation = @id_operation   and Ltrim(rtrim(number_trek)) = @number_trek and ST='У'
		AND pornom=@PORNOM 
   end
 end


END

