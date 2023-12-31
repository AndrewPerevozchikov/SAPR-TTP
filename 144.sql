USE [PCTEXT]
GO
/****** Object:  StoredProcedure [dbo].[e012p144_TreeStandartOperation]    Script Date: 06.10.2023 14:19:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Процедура формирования дерева типовых операций или переходов
-- Автор: Перевозчиков Андрей Иванович 556176
-- Дата создания: 29.08.2023 
-- Входные параметры: 
-- @NOM_MAS int  номер массива

-- =============================================
ALTER       PROCEDURE [dbo].[e012p144_TreeStandartOperation]
	 @NOM_MAS int
AS
BEGIN
/*
 declare 
 @NOM_MAS int
 
 set @NOM_MAS =29
 */
 
 --declare 
  --@prgrtp smallint -- признак ТП( единичный или групповой (типовой))

 --
  CREATE TABLE #Temp(
	[NOM_MAS] [int] NOT NULL,
	[number_operation] [smallint] NOT NULL,
	[NC] [char](3) NULL,
	[KOD_OPER] [char](4) NULL,
	[NMOPR] [char](100) NULL,
	[OBDOK] [char](59) NULL,
	[NIOT] [smallint] NULL,
	[VID_PR] [char](32) NULL,
	[number_operationTI] [smallint] NULL,
	[LASTDATA] [datetime] NULL,
	[KOMM] [char](200) NULL,
	[IDKOR] [char](12) NULL,
	[DKOR] [datetime] NULL,
	[id_operation] [uniqueidentifier] NULL)

insert #Temp
SELECT *
  FROM [PCTEXT].[dbo].[operation]

 -- дерево операций  
 create table #treeoper

  (position smallint null,
  Level smallint null,
  Tip smallint null,
   NameChapter varchar(max),
   id_operation uniqueidentifier NULL,
   id_number_trek uniqueidentifier NULL,
   number_trek char(3) null,
   pornom smallint NULL,
   number_operation smallint NULL
   )

 INSERT #treeoper (position,Level,Tip,NameChapter) VALUES (1,0,5,'ОПЕРАЦИИ [A]')

--добавление ОПЕРАЦИИ 
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 1,1,10,'А' + ' ' + ltrim(rtrim(c.KOD_OPER)) +' ' + ltrim(rtrim(c.nmopr)),c.id_operation,c.number_operation,0
   FROM  operation c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS
   Order by c.KOD_OPER, c.nmopr

--добавление ОБОРУДОВАНИЕ 
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 2,3,13,'Б' +'  '+ltrim(rtrim(kod))+'  '++ltrim(rtrim(nst))+'  '+ ltrim(rtrim(kprof))
,c.id_operation,c.number_operation,PORNOM
   FROM  eqipment c
   LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS

   --добавление МАТЕРИАЛЫ  
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 3,3,17,'М'+'  '+ltrim(rtrim(case when b.id_nn_material is null then b.naimc else b1.nd end))+ltrim(rtrim(vr))
,b.id_operation,b.number_operation,PORNOM
   FROM  material b
   left join  kltmcm b1 on b1.id_gpf=b.id_nn_material              
   WHERE  @NOM_MAS=NOM_MAS

   --добавление ОСНАСТКИ  
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 4,3,19,'Т'+'  '+ltrim(rtrim(shc))+'  '++ltrim(rtrim(naimcto))
,c.id_operation,c.number_operation,PORNOM
   FROM  prop c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  (ltrim(rtrim(number_trek))='' or number_trek is null)

   --добавление РЕЖИМЫ   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 5,3,21,'Р'+'  '+ltrim(rtrim(SODRE))
,c.id_operation,c.number_operation,PORNOM
   FROM  mode c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  (ltrim(rtrim(number_trek))='' or number_trek is null)

   --добавление ВНИМАНИЕ    
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 6,3,26,FREETEX
,c.id_operation,c.number_operation,PORNOM
   FROM  operationFT c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  (ST='В' or ST=' В')

   --добавление ПЕРЕХОДЫ   	
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,id_number_trek,number_trek)
   SELECT 7,3,24,'О '+'  '+ ltrim(rtrim(number_trek))+' '+ltrim(rtrim(CAST(information AS VARCHAR(MAX))))
,c.id_operation,c.number_operation,id_number_trek,number_trek
   FROM  trek c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation 
  WHERE  @NOM_MAS=c.NOM_MAS  

   --добавление РЕЖИМЫ в переходах  
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,id_number_trek,number_trek)
   SELECT 8, 5,21,'Р'+'  '+ltrim(rtrim(SODRE))
,c.id_operation,c.number_operation,PORNOM,id_number_trek,number_trek
   FROM  mode c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  ltrim(rtrim(number_trek))<>''

   --добавление УКАЗАНИЯ в переходах    
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,id_number_trek,number_trek)
   SELECT  9,5,27,FREETEX
,c.id_operation,c.number_operation,PORNOM,id_number_trek,number_trek
   FROM  trekFT c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  (ST='У' or ST=' У') 

   --добавление ОСНАСТКИ  в переходах
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,id_number_trek,number_trek)
   SELECT 10,5,19,'Т'+'  '+ltrim(rtrim(shc))+'  '++ltrim(rtrim(naimcto))
,c.id_operation,c.number_operation,PORNOM,id_number_trek,number_trek
   FROM  prop c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  ltrim(rtrim(number_trek))<>''


   --добавление УКАЗАНИЯ    
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT  11,3,27,FREETEX
,c.id_operation,c.number_operation,PORNOM
   FROM  operationFT c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND  (ST='У' or ST=' У')


--добавление заголовка ОБОРУДОВАНИЯ
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 2,2,12,'ОБОРУДОВАНИЕ [Б]'
,c.id_operation,c.number_operation,0
   FROM  eqipment c
   LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS

   --добавление заголовка МАТЕРИАЛЫ 
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 3,2,16,'МАТЕРИАЛЫ  [М]'
,c.id_operation,c.number_operation,0
   FROM  material c
   LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS

   --добавление заголовка ОСНАСТКА 
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 4,2,18,'ОСНАСТКА [Т]'
,c.id_operation,c.number_operation,0
   FROM  prop c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND (ltrim(rtrim(number_trek))='' or number_trek is null)

      --добавление заголовка РЕЖИМЫ  
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 5,2,18,'РЕЖИМЫ [Р]'
,c.id_operation,c.number_operation,0
   FROM  mode c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND (ltrim(rtrim(number_trek))='' or number_trek is null)

   --добавление заголовка ВНИМАНИЕ    
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 6,2,22,'ВНИМАНИЕ [В]',c.id_operation,c.number_operation,0
   FROM  operationFT c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND (ST='В' or ST=' В')


   --добавление заголовка ПЕРЕХОДЫ   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 7, 2,23,'ПЕРЕХОДЫ  [О]'
,c.id_operation,c.number_operation,0
   FROM  trek c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS 

   --добавление заголовка РЕЖИМЫ [Р] в переходах   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 8, 4,20,'РЕЖИМЫ [Р]'
,c.id_operation,c.number_operation,0
   FROM  mode c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND ltrim(rtrim(number_trek))<>''

    --добавление заголовка УКАЗАНИЕ [У] в переходах     
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 9, 4,25,'УКАЗАНИЕ [У] '
,c.id_operation,c.number_operation,0
   FROM  trekFT c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND (ST='У' or ST=' У')

   --добавление заголовка ОСНАСТКА [Т] в переходах   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT DISTINCT 10, 4,18,'ОСНАСТКА [Т]'
,c.id_operation,c.number_operation,0
   FROM  prop c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND ltrim(rtrim(number_trek))<>''



   --добавление заголовка УКАЗАНИЕ [У] в переходах     
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom)
   SELECT 11, 2,25,'УКАЗАНИЕ [У] '
,c.id_operation,c.number_operation,0
   FROM  operationft c
  LEFT JOIN #Temp d on c.id_operation=d.id_operation  WHERE  @NOM_MAS=c.NOM_MAS AND (ST='У' or ST=' У')
  

--вывод данных
SELECT Level,Tip,NameChapter,id_operation,id_number_trek,number_trek,pornom from #treeoper 
--WHERE id_number_trek<>null
order by number_operation,position,number_trek,tip,level
--удаление временных таблиц
DROP TABLE #treeoper
DROP TABLE #Temp

    
END

