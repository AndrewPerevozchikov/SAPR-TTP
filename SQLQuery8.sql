USE [SKAT]
GO
/****** Object:  StoredProcedure [dbo].[e012p141_treeTTP]    Script Date: 06.10.2023 14:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Процедура формирования дерева ТТП
-- Автор: Перевозчиков Андрей Иванович 556176
-- Дата создания: 14.08.2023 
-- Входные параметры: 
--    @name_dse  - обозначение ДСЕ
--    @type_work - вид технологии
-- =============================================
ALTER     PROCEDURE [dbo].[e012p141_treeTTP]
	@name_dse char(24),@type_work char(2)
AS
BEGIN
	 
/* declare 
 @name_dse char(24),@type_work char(2)
 set @name_dse='ЦВИЯ685624021'
-- set @name_dse='ЦВИЯ685631473' 
 set @type_work='28'
 
-- для проверки таблиц в дереве
 set @name_dse='ТТП-1164'
 set @type_work='28'*/
 
 declare 
  @prgrtp smallint -- признак ТП( единичный или групповой (типовой))

 --список операций
 CREATE TABLE #Lispoper 
	(name_dse char(24) NULL,
     type_work char(2) NULL,
	 number_operation smallint NULL,
	 version char(2) NULL,	
	 nmopr char(100) NULL,	
	 id_operation uniqueidentifier NULL)

 --добавление операций во временную таблицу
 INSERT	#Lispoper
   SELECT c.name_dse,c.type_work,c.number_operation,c.version,c.nmopr,c.id_operation
   FROM  operation c
   WHERE  c.type_work=@type_work AND c.name_dse=@name_dse

 -- дерево операций  
 create table #treeoper
  (Level smallint null,
   Tip smallint null,
   NameChapter varchar(max),
   name_dse char(24) NULL,
   type_work char(2) NULL,
   number_operation smallint  null,
   number_trek char(3) null,
   version char(2) NULL,
   pornom smallint NULL,
   id_operation uniqueidentifier NULL,
   id_nn_material uniqueidentifier NULL,
   position int null,
   perehodPosition int null)

  -- дерево ТП
 create table #Treetp
(Level smallint null,
 Tip smallint null,
 NameChapter varchar(max),
 name_dse char(24) NULL,
 type_work char(2) NULL,
 number_operation smallint  null,
 number_trek char(3) null,
 version char(2) NULL,
 pornom smallint NULL,
 id_operation uniqueidentifier NULL,
 id_nn_material uniqueidentifier NULL,
 Ordernumber int IDENTITY (1, 1))

 --определение ТП единичный или групповой (типовой) 
 select @prgrtp=prgrtp  FROM  TLP  WHERE  name_dse=@name_dse AND type_work=@type_work
 
 --добавление 0 уровня
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,0,'ОБЩИЕ СВЕДЕНИЯ')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (1,7,'ОБЩИЕ ДАННЫЕ')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (1,8,'ПОДПИСИ')
 if @prgrtp=0 --единичный
    INSERT #Treetp (Level,Tip,NameChapter) VALUES (1,9,'ИСПОЛНЕНИЯ')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,1,'ТТ')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,2,'ТТБ')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,3,'ТЭБ')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,4,'СВОБОДНЫЙ ТЕКСТ (ко всему ТП)')
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,5,'ОПЕРАЦИИ [A]')

 --добавление операции
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,id_operation) 
	SELECT 1,1,10,
	'А'+'  '+ replace(str(c.number_operation,3),' ','0')+ '  '+ ltrim(rtrim(c.nmopr)),
	c.name_dse, c.type_work,c.number_operation,c.version,c.id_operation
	FROM #Lispoper c

 --добавление оборудования  
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT 2,3,13, 'Б'+'  '+ltrim(rtrim(kod))+'  '++ltrim(rtrim(nst))+'  ('+ ltrim(rtrim(kprof))+')', 
	c.name_dse, c.type_work, c.number_operation,c.version,b.pornom,c.id_operation
	FROM #Lispoper c
	JOIN  eqipment b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 

 --добавление тех.деталей
 if  @prgrtp=0 -- единичный 
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT 3,3,15, 'К'+'  '+ltrim(rtrim(naim))+'  '++ltrim(rtrim(gost))+'  '+ ltrim(rtrim(dsek)), 
	c.name_dse, c.type_work,c.number_operation,c.version,b.pornom,c.id_operation
	FROM #Lispoper c
	JOIN  details b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 

--добавление материалов
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation,id_nn_material) 
       SELECT 4,3,17, 'М'+'  '+ltrim(rtrim(case when b.id_nn_material is null then b.naimc else b1.nd end))+'  '++ltrim(rtrim(vr)), 
       c.name_dse, c.type_work,c.number_operation,c.version,b.pornom,c.id_operation,b.id_nn_material
       FROM #Lispoper c
       JOIN  material b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
       left join  kltmcm b1 on b1.id_gpf=b.id_nn_material               


 --добавление оснастки
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation) 
	SELECT 5,3,19, 'Т'+'  '+ ltrim(rtrim(shc))+'  '++ltrim(rtrim(naimcto)), 
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation
	FROM #Lispoper c
	JOIN  prop b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE ltrim(rtrim(number_trek))='' or number_trek is null

 --добавление режимов 
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation) 
	SELECT 6,3,21, 'Р'+'  '+ltrim(rtrim(SODRE)), 
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation
	FROM #Lispoper c
	JOIN  mode b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE ltrim(rtrim(number_trek))='' or number_trek is null

 --добавление внимание
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT 7, 3,26, b.Freetex,
	c.name_dse, c.type_work, c.number_operation,c.version,b.pornom,c.id_operation
	FROM #Lispoper c
	JOIN  operationft b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE st='В'
   
 --добавление переходов
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,id_operation) 
	SELECT 8,3,24, 'О'+' '+ ltrim(rtrim(number_trek))+' '+ltrim(rtrim(CAST(information AS VARCHAR(MAX)))), 
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,c.id_operation
	FROM #Lispoper c
	JOIN  trek b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 

 --добавление режимов в переходах 
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
	SELECT 8,5,21, 'Р'+'  '+ltrim(rtrim(SODRE)), 
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation,1
	FROM #Lispoper c
	JOIN  mode b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE ltrim(rtrim(number_trek))<>'' and number_trek is not null

 --добавление указаний к переходам
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
	SELECT 8, 5,27, b.freetex, 
	c.name_dse,c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation,2
	FROM #Lispoper c
	JOIN  trekft b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
	WHERE st='У'  AND ltrim(rtrim(number_trek))<>'' and number_trek is not null

 --добавление оснастки для переходов
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
	SELECT 8,5,19, 'Т'+'  '+ ltrim(rtrim(shc))+'  '++ltrim(rtrim(naimcto)), 
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation,3
	FROM #Lispoper c
	JOIN  prop b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE ltrim(rtrim(number_trek))<>'' and number_trek is not null
	
 --добавление эскизов для переходов
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
	SELECT 8,5,11, 'Э'+'  ЭСКИЗ '+ltrim(rtrim(pornom)),
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation,4
	FROM #Lispoper c
	JOIN  BESKIZ b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE ltrim(rtrim(number_trek))<>'' and number_trek is not null

--добавление указаний
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
  SELECT 13, 3,27, b.freetex, c.name_dse, c.type_work, c.number_operation,c.version,b.pornom,c.id_operation
  FROM #Lispoper c
  JOIN  operationft b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
  WHERE st='У' 

 --добавление эскизов к операции
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation) 
	SELECT 14,3,11, 'Э'+'  ЭСКИЗ '+ltrim(rtrim(pornom)),
	c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,b.pornom,c.id_operation
	FROM #Lispoper c
	JOIN  BESKIZ b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
	WHERE ltrim(rtrim(number_trek))='' or number_trek is null
	
 --добавление заголовка оборудования
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT DISTINCT 2,2,12,'ОБОРУДОВАНИЕ [Б]', 
	c.name_dse, c.type_work,c.number_operation,c.version,0,c.id_operation
	FROM #Lispoper c
	JOIN  eqipment b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 

 --добавление заголовка тех деталей
 if  @prgrtp=0 -- единичный 
  INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT DISTINCT 3,2,14,'ТЕХ ДЕТАЛИ [К]', 
	c.name_dse, c.type_work,c.number_operation,c.version,0,c.id_operation
	FROM #Lispoper c
	INNER JOIN  details b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 

 --добавление заголовка тех материалов
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT DISTINCT 4,2,16,'МАТЕРИАЛЫ [М]', 
	c.name_dse, c.type_work,c.number_operation,c.version,0,c.id_operation
	FROM #Lispoper c
	JOIN  material b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version

 --добавление заголовка оснастки
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT DISTINCT 5,2,18,'ОСНАСТКА [Т]', 
	c.name_dse, c.type_work,c.number_operation,c.version,0,c.id_operation
	FROM #Lispoper c
	JOIN  prop b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
	WHERE ltrim(rtrim(number_trek))='' or number_trek is null

 --добавление заголовка режимов
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT DISTINCT 6,2,20,'РЕЖИМЫ [Р]', 
	c.name_dse, c.type_work,c.number_operation,c.version,0,c.id_operation
	FROM #Lispoper c
	JOIN  mode b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
	WHERE ltrim(rtrim(number_trek))='' or number_trek is null

 --добавление заголовка внимание
 INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
	SELECT DISTINCT 7,2,22,'ВНИМАНИЕ [В] ', 
	c.name_dse, c.type_work,c.number_operation,c.version,0,c.id_operation
	FROM #Lispoper c
	JOIN  operationft b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
	WHERE st='В'

 --добаление заголовка переходов
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation) 
 SELECT DISTINCT 8,2,23,'ПЕРЕХОДЫ [О]', 
 c.name_dse, c.type_work,c.number_operation,'',c.version,0,c.id_operation
 FROM #Lispoper c
JOIN  trek b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version

--добавление заголовка режимов в переходах 
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
 SELECT DISTINCT 8,4,20, 'РЕЖИМЫ [Р] (в переходах)', 
 c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,0,c.id_operation,1
 FROM #Lispoper c
 JOIN  mode b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version 
 WHERE ltrim(rtrim(number_trek))<>'' and number_trek is not null

--добавление заголовка указаний к переходам
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
 SELECT DISTINCT 8, 4,25,  'УКАЗАНИЕ [У] (к переходам)' ,
 c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,0,c.id_operation,2
 FROM #Lispoper c
 JOIN  trekft b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
 WHERE st='У' 

--добавление оснастки для переходов
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
 SELECT  DISTINCT 8,4,18, 'ОСНАСТКА [Т] (в переходах)', 
 c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,0,c.id_operation,3
 FROM #Lispoper c
 JOIN  prop b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version  
 WHERE ltrim(rtrim(number_trek))<>'' and number_trek is  not null

--добавление заголовка эскизов к переходам
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,perehodPosition) 
 SELECT DISTINCT 8,4,6,'ЭСКИЗЫ [Э] (к переходам)', 
 c.name_dse, c.type_work,c.number_operation,b.number_trek,c.version,0,c.id_operation,4
 FROM #Lispoper c
 JOIN  BESKIZ b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
 WHERE ltrim(rtrim(number_trek))<>'' and number_trek is not null

 --добавление заголовка указаний к операциям
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,pornom,id_operation) 
 SELECT DISTINCT 13, 2,25,  'УКАЗАНИЕ [У] (к операциям)',
 c.name_dse, c.type_work, c.number_operation,c.version,0,c.id_operation
 FROM #Lispoper c
 JOIN  operationft b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version
 WHERE st='У' 

 

--добавление заголовка эскизов к операции
INSERT #treeoper (position,Level,Tip,NameChapter,name_dse,type_work,number_operation,version,number_trek,pornom,id_operation) 
 SELECT DISTINCT 13,2,6,'ЭСКИЗЫ [Э] (к операции)', 
 c.name_dse, c.type_work,c.number_operation,c.version,number_trek,0,c.id_operation
 FROM #Lispoper c
 JOIN  BESKIZ b ON c.name_dse=b.name_dse And c.type_work=b.type_work AND c.number_operation=b.number_operation AND c.version = b.version  
 WHERE ltrim(rtrim(number_trek))='' or number_trek is null
  
  --добавление операций  в дерево ТП
INSERT #Treetp
 SELECT t.Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation,id_nn_material
 FROM #treeoper t 
 order by number_operation,position,number_trek,perehodPosition, pornom,Tip
 --добавление заголовка эскизов
INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,6,'ЭСКИЗЫ [Э] (ко всему ТП)')
 --добавление эскизов
INSERT #Treetp (Level,Tip,NameChapter,name_dse,type_work,number_operation,number_trek,version,pornom,id_operation) 
 SELECT 1,11, 'Э'+'  ЭСКИЗ '+ltrim(rtrim(pornom)),
 name_dse,type_work,number_operation,number_trek,version,pornom,id_operation
 FROM  BESKIZ 
 WHERE number_operation=1000 and name_dse=@name_dse And type_work=@type_work

  --добавление заголовка таблицы КТП
 INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,29,'ТАБЛИЦЫ [КТП]')
--добавление таблицы КТП
 INSERT #Treetp (Level,Tip,NameChapter,name_dse,type_work,version) 
 SELECT 1,30, 'КТП.'+ltrim(str(Number_Table,3))+'  '+ltrim(rtrim(Name_Table)),
 name_dse,type_work,version
 FROM  contenttable c
 WHERE name_dse=@name_dse And type_work=@type_work AND PRIM=1
 order by Number_Table

 if @type_work in('05','30')  -- табличные ГТП 
  begin
	--добавление заголовка таблицы КТП
	INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,31,'ТАБЛИЦЫ [ВТП]')
	--добавление таблицы КТП
	INSERT #Treetp (Level,Tip,NameChapter,name_dse,type_work,version) 
	SELECT 1,32, 'ВТП.'+ltrim(str(Number_Table,3))+'  '+ltrim(rtrim(Name_Table)),
	name_dse,type_work,version
	FROM  contenttable c
	WHERE name_dse=@name_dse And type_work=@type_work AND PRIM=2
	order by Number_Table
  end
  
--добавление листа изменений
INSERT #Treetp (Level,Tip,NameChapter) VALUES (0,28,'ЛИСТ ИЗМЕНЕНИЙ')

SELECT * FROM #Treetp order by Ordernumber

--удаление временных таблиц
DROP TABLE #Treetp
DROP TABLE #treeoper
DROP TABLE #Lispoper
END
