USE [SKAT]
GO
/****** Object:  StoredProcedure [dbo].[e012p142_etTipTreeTTP]    Script Date: 06.10.2023 14:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Процедура получение данных из дерева ТТП по типу
-- Автор: Перевозчиков Андрей Иванович 556176
-- Дата создания: 21.08.2023 
-- Входные параметры: 
--  @name_dse  - обозначение ДСЕ,
--	@type_work - вид технологии
--	@Tip – тип переменной (пункт дерева)
--	@Level – уровень переменной
--	@version – версия документа
--	@id_operation – идентификатор  операции 
--	@number_trek – номер перехода (0- все, иначе конкретный) (если нет перехода передавать 0)
--	@pornom – порядковый номер (0- все, иначе конкретный)
--	@attribute_sign –признак подписи (0-автор, 1-соисполнители, 2- согласующие)
--	@attribute_doc –признак документа (1-ТП,2-извещение)
--	@id_table –идентификатор таблицы

-- =============================================
ALTER     PROCEDURE [dbo].[e012p142_etTipTreeTTP]
	@name_dse char(24),
	@type_work char(2),
	@TIP smallint,
	@Level smallint,
	@id_operation uniqueidentifier,
	@number_trek char(3),--отправлять без пробелов
	@PORNOM smallint,
	@ATTRIBUTE_SINDL smallint,
	@ATTRIBUTE_DOC smallint,
	@ID_TABLE uniqueidentifier
AS
BEGIN

/*declare

@name_dse char(24),
@type_work char(2),
@TIP smallint,
@Level smallint,
@version char(2),
@id_operation uniqueidentifier,
@number_trek char(3),
@PORNOM smallint,
@ATTRIBUTE_SINDL smallint,
@ATTRIBUTE_DOC smallint,
@ID_TABLE uniqueidentifier

set @name_dse='ЦВИЯ685624021'
set @type_work='28'
set @Level = 0
set @TIP=21
set @version ='00'
set @id_operation='79896D89-9CFC-41DD-895B-A7FF81537C6E'
set @number_trek = 1
set @pornom = 1
set @ATTRIBUTE_SINDL = 0
set @ATTRIBUTE_DOC = 0
set @ID_TABLE ='5A55DCA2-A633-4F97-8634-B44BF6C6DAE8'*/

/* -- для проверки таблиц в дереве
set @name_dse='ГТО-123'
set @type_work='30'
set @Level = 1
set @TIP=30
set @version ='00'
set @id_operation='79896D89-9CFC-41DD-895B-A7FF81537C6E'
set @number_trek = 0
set @pornom = 0
set @ATTRIBUTE_SINDL = 0
set @ATTRIBUTE_DOC = 0
set @ID_TABLE ='9F95B46F-9DB6-40DC-8B1A-5A11D47E279B'*/


 --tip=1 ТТ
if(@TIP=1)
select s.*,idavtor,fioavtor,dpavtor,number_operationst_st from  permission s
LEFT join BDOLOG b on b.priz=s.priz and b.name_dse=s.name_dse And b.type_work=s.type_work AND b.punkt=s.punkt
WHERE s.priz=1 and s.name_dse=@name_dse And s.type_work=@type_work
order by s.punkt

--tip=2 ТТБ
if(@TIP=2)
select s.*,idavtor,fioavtor,dpavtor,number_operationst_st from  permission s
LEFT join BDOLOG b on b.priz=s.priz and b.name_dse=s.name_dse And b.type_work=s.type_work AND b.punkt=s.punkt
WHERE s.priz=2 and s.name_dse=@name_dse And s.type_work=@type_work
order by s.punkt

--tip=3 ТЭБ
if(@TIP=3)
select s.*,idavtor,fioavtor,dpavtor,number_operationst_st from  permission s
LEFT join BDOLOG b on b.priz=s.priz and b.name_dse=s.name_dse And b.type_work=s.type_work AND b.punkt=s.punkt
WHERE s.priz=3 and s.name_dse=@name_dse And s.type_work=@type_work
order by s.punkt


--tip=4 Свободный текст ко всему ТП
if(@TIP=4)
select * from  operationft
WHERE  name_dse=@name_dse And type_work=@type_work AND number_operation=1000
order by pornom


--tip=7 Общие данные
if(@TIP=7)
select *,No, PO, prgrtp from  bod b
join [SKAT].[dbo].Tlp g  on  g.name_dse=b.name_dse And g.type_work=b.type_work 
left join [SKAT].[dbo].[spr_obrab] s on  b.met_obrab = s.met_obrab
WHERE  b.name_dse=@name_dse And b.type_work=@type_work 


--tip=8 подписи
if(@TIP=8)
  begin
    --автор
    if @ATTRIBUTE_SINDL=0
	  if @ATTRIBUTE_DOC=1 --ТП,иначе извещение
	    SELECT *
	     FROM  Tlp where name_dse=@name_dse and type_work=@type_work
      else
	    select * FROM  operationp where name_dse=@name_dse and type_work=@type_work and version = (select max(b.version) from  operationp b where b.name_dse = @name_dse and b.type_work = @type_work)

    -- соисполнитель 
    if @ATTRIBUTE_SINDL=1
	 if @ATTRIBUTE_DOC=1 --ТП,иначе извещение
	  SELECT *
	    FROM  soisp where name_dse=@name_dse and type_work=@type_work and version='00'
     else
	  SELECT *
	  FROM  soisp where name_dse=@name_dse and type_work=@type_work and version = (select max(b.version) from  soisp b where b.name_dse = @name_dse and b.type_work = @type_work)

   -- согласующие 
   if @ATTRIBUTE_SINDL=2 --ТП,иначе извещение
	if @ATTRIBUTE_DOC=1
	 SELECT *
	   FROM  sogltp where name_dse=@name_dse and type_work=@type_work
	   order by nazsogl
    else
	 SELECT *
	  FROM  soglizv where name_dse=@name_dse and type_work=@type_work  
	  order by nazsogl	
  end

--tip 9 Исполнения
if(@TIP=9)
select * from  boddse
WHERE  name_dse=@name_dse And type_work=@type_work
order by name_dseisp

--tip 10 Операции
if(@TIP=10) 
select s.*,idavtor,fioavtor,dpavtor,priz,punkt from  operation s
left join BDOLOG d on s.id_operation=d.id_operation and PRIZ=0
WHERE  s.name_dse=@name_dse And s.type_work=@type_work AND s.id_operation=@id_operation

--tip 11 Эскиз к переходам 
if @TIP=11
 begin
  if @Level = 1  -- ко всему ТП
   if @PORNOM = 0 --все, иначе конкретный
     select * from  beskiz
      WHERE  name_dse=@name_dse And type_work=@type_work AND number_operation = 1000
	  order by pornom
   else
     select * from  beskiz
      WHERE  name_dse=@name_dse And type_work=@type_work AND pornom = @PORNOM AND number_operation = 1000
  if @Level = 3  -- к операции
     if @PORNOM = 0 --все, иначе конкретный
	   select * from  beskiz
         WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and (ltrim(rtrim(number_trek)) ='' or  number_trek is null)
		 order by pornom
     else
	   select * from  beskiz
         WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and pornom = @PORNOM
         and (ltrim(rtrim(number_trek)) ='' or  number_trek is null)
   if @Level = 5  -- к переходу
     if @PORNOM = 0 --все, иначе конкретный
	 select * from  beskiz
         WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and ltrim(rtrim(number_trek))=@number_trek
		 order by pornom
     else
	   select * from  beskiz
         WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and ltrim(rtrim(number_trek))=@number_trek 
		 and pornom = @PORNOM
 end        


--tip 13 Оборудование
if(@TIP=13) 
 if(@PORNOM = 0)
  select * from  eqipment
   WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation
   order by pornom
 else
  select * from  eqipment
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM
	order by pornom

--tip 15 Технологические детали
if(@TIP=15)
 if(@PORNOM = 0)
  select * from  details
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation
	order by pornom
else
  select * from  details
  WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM


--tip 17 Материалы
if(@TIP=17)
 if(@PORNOM = 0)
    SELECT name_dse,type_work,number_operation,version,pornom,
    naimc=ltrim(rtrim(case when b.id_nn_material is null then b.naimc else b1.nd end)),
    ei,vr,en,pokr,krnaimc,number_operationst_st,protctp,datkor,ki,id_operation,id_nn_material,handle_input,
    Concetration,Viscosity,Density
    FROM  material b
	left join  kltmcm b1 on b1.id_gpf=b.id_nn_material  
	order by pornom
 else
  SELECT name_dse,type_work,number_operation,version,pornom,
    naimc=ltrim(rtrim(case when b.id_nn_material is null then b.naimc else b1.nd end)),
    ei,vr,en,pokr,krnaimc,number_operationst_st,protctp,datkor,ki,id_operation,id_nn_material,handle_input,
    Concetration,Viscosity,Density
    FROM  material b
	left join  kltmcm b1 on b1.id_gpf=b.id_nn_material  
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM

--tip 19 Оснастка 
if(@TIP=19)
 begin
  if @level=3 --к операции, иначе к переходам
   begin
	 if(@PORNOM = 0)
	   select * from  prop
		WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
		order by pornom
	 else
	   select * from  prop
		WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM 
		and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
   end
  else
   begin
	 if(@PORNOM = 0)
	   select * from  prop
		WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) =@number_trek
		order by pornom
	 else
	   select * from  prop
		WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) =@number_trek 
		AND pornom=@PORNOM 
   end
 end


--tip 21 Режимы 
if(@TIP=21)
 begin
  if @level=3 --к операции, иначе к переходам
   begin
    if(@PORNOM = 0)
      select * from  mode
       WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
	   order by pornom
    else
     select * from  mode
      WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM and (Ltrim(rtrim(number_trek)) ='' or  number_trek is null)
   end
  else
   begin
    if(@PORNOM = 0)
      select * from  mode
       WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) =@number_trek
	   order by pornom
    else
     select * from  mode
      WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM and Ltrim(rtrim(number_trek)) =@number_trek
   end
end

--tip 24 Переходы
if(@TIP=24)
 if(@number_trek = '0')
  select * from  trek
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation 
    order by number_trek
 else
  select * from  trek
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) =@number_trek



--tip 26 Свободный  текст к операции (ВНИМАНИЕ [В] )
if(@TIP=26) 
 if(@PORNOM = 0)
   select * from  operationFT
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND st='В'
 else
  select * from  operationFT
    WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM  AND st='В'

--tip 27 Свободный  текст (УКАЗАНИЕ [У]) 
if(@TIP=27)
 begin
  if @level=3 --к операции, иначе к переходам
   begin
    if(@PORNOM = 0)
      select * from  operationFT
       WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND st='У'
    else
     select * from  operationFT
       WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM  AND st='У'
   end	
  else
   begin
    if(@PORNOM = 0)
      select * from  trekft
       WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation and Ltrim(rtrim(number_trek)) =@number_trek
	   order by pornom
    else
     select * from  trekft
      WHERE  name_dse=@name_dse And type_work=@type_work AND id_operation=@id_operation AND pornom=@PORNOM and Ltrim(rtrim(number_trek)) =@number_trek
   end
 end 	
	

--tip=28 ЛИСТ изменений
if(@TIP=28)
 select * from  operationp
   WHERE  name_dse=@name_dse And type_work=@type_work 
   order by version 
   


--tip=30 ТАБЛИЦЫ [КТП], tip=32 ТАБЛИЦЫ [ВТП]
if(@TIP=30) or (@TIP=32)
 select * from  Tablegtp
   WHERE  id_table=@ID_TABLE  
   order by pornom     
END

