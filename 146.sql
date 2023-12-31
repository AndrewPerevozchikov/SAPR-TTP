USE [PCTEXT]
GO
/****** Object:  StoredProcedure [dbo].[e012p146_GetTipStandartTransition]    Script Date: 06.10.2023 14:19:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Процедура получение переходов из дерева стандартных ТТП по типу
-- Автор: Перевозчиков Андрей Иванович 556176
-- Дата создания: 13.09.2023 
-- Входные параметры: 
--@nom_mas  - номер массива,
--@Tip – тип переменной (пункт дерева)
--@id_number_trek – идентификатор  перехода 
--@pornom – порядковый номер (0- все, иначе конкретный)

-- =============================================
ALTER     PROCEDURE [dbo].[e012p146_GetTipStandartTransition]
	@NOM_MAS int,
@TIP smallint,
@ID_number_trek uniqueidentifier,
@PORNOM smallint
AS
BEGIN
	--tip=24 ПЕРЕХОДЫ [О]
if(@TIP=24)
select * from  trek
WHERE NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek 

--tip=21 РЕЖИМЫ [Р]
if(@TIP=21)
if @PORNOM = 0 --все, иначе конкретный
     select * from  mode
      WHERE  NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek
	  order by pornom
   else
     select * from  mode
      WHERE  NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek AND pornom = @PORNOM 
--tip=27 УКАЗАНИЕ [У] 
if(@TIP=27)
if @PORNOM = 0 --все, иначе конкретный
     select * from  trekFT
      WHERE  NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek AND ST='У'
	  order by pornom
   else
     select * from  trekFT
      WHERE  NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek AND pornom = @PORNOM AND ST='У'
--tip=19 ОСНАСТКА [Т] 
if(@TIP=19)
if @PORNOM = 0 --все, иначе конкретный
     select *,pr_name=(case when ipr=1 then 'ПРИСПОСОБЛЕНИЯ'
									  when ipr=2 then 'ВСПОМОГАТЕЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=3 then 'РЕЖУЩИЙ ИНСТРУМЕНТ'
									  when ipr=4 then 'СЛЕСАРНО-МОНТАЖНЫЙ ИНСТРУМЕНТ'
									  when ipr=5 then 'СПЕЦИАЛЬНЫЙ ИНСТРУМЕНТ'
									  when ipr=6 then 'МЕРИТЕЛЬНЫЙ ИНСТРУМЕНТ'
					                  else 'ПРИСПОСОБЛЕНИЯ' end) 
	  from  PROP
      WHERE  NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek
	  order by pornom
   else
     select * from  PROP
      WHERE  NOM_MAS = @NOM_MAS And id_number_trek = @id_number_trek AND pornom = @PORNOM 
END

