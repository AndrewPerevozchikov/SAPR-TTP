USE [PCTEXT]
GO
/****** Object:  StoredProcedure [dbo].[e012p143_TreeStandartTransition]    Script Date: 06.10.2023 14:19:48 ******/
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
ALTER     PROCEDURE [dbo].[e012p143_TreeStandartTransition]
	@NOM_MAS int
AS
BEGIN
	 /*declare 
 @NOM_MAS int
 
 set @NOM_MAS =14*/
 

 CREATE TABLE #Temp(
	[NOM_MAS] [int] NULL,
	[number_operation] [smallint] NULL,
	[number_trek] [char](3) NULL,
	[information] [text] NULL,
	[lastdata] [datetime] NULL,
	[komm] [char](200) NULL,
	[IDKOR] [char](12) NULL,
	[DKOR] [datetime] NULL,
	[id_operation] [uniqueidentifier] NULL,
	[id_number_trek] [uniqueidentifier] NULL
)


	insert #Temp
SELECT *
  FROM  trek
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



  --добавление ПЕРЕХОДЫ   	
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,id_number_trek,number_trek)
   SELECT 7,1,24,'О '+'  '+ ltrim(rtrim(c.number_trek))+' '+ltrim(rtrim(CAST(c.information AS VARCHAR(MAX))))
,c.id_operation,c.number_operation,c.id_number_trek,c.number_trek
   FROM  trek c
  LEFT JOIN #Temp d on c.id_number_trek=d.id_number_trek 
  WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0

   --добавление РЕЖИМЫ в переходах  
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,id_number_trek,number_trek)
   SELECT 8,3,21,'Р'+'  '+ltrim(rtrim(SODRE))
,c.id_operation,c.number_operation,c.PORNOM,c.id_number_trek,c.number_trek
   FROM  mode c
  LEFT JOIN #Temp d on c.id_number_trek=d.id_number_trek  
  WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 AND  ltrim(rtrim(c.number_trek))<>''

   --добавление УКАЗАНИЯ в переходах    
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,id_number_trek,number_trek)
   SELECT  9,3,27,c.FREETEX
,c.id_operation,c.number_operation,c.PORNOM,c.id_number_trek,c.number_trek
   FROM  trekFT c
  LEFT JOIN #Temp d on c.ID_number_trek=d.ID_number_trek  WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 AND  (ST='У' or ST=' У') 

   --добавление ОСНАСТКИ  в переходах
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,id_number_trek,number_trek)
   SELECT 10,3,19,'Т'+'  '+ltrim(rtrim(shc))+'  '++ltrim(rtrim(naimcto))
,c.id_operation,c.number_operation,c.PORNOM,c.id_number_trek,c.number_trek
   FROM  prop c
  LEFT JOIN #Temp d on c.ID_number_trek=d.ID_number_trek  WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 AND  ltrim(rtrim(c.number_trek))<>''


   --добавление заголовка ПЕРЕХОДЫ   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,number_trek)
   SELECT DISTINCT 7,0,23,'ПЕРЕХОДЫ  [О]'
,c.id_operation,c.number_operation,0,c.number_trek
   FROM  trek c
   LEFT JOIN #Temp d on c.id_number_trek=d.id_number_trek
   WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 

   --добавление заголовка РЕЖИМЫ [Р] в переходах   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,number_trek)
   SELECT DISTINCT 8,2,20,'РЕЖИМЫ [Р]'
,c.id_operation,c.number_operation,0,c.number_trek
   FROM  mode c
   LEFT JOIN #Temp d on c.id_number_trek=d.id_number_trek
   WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 and ltrim(rtrim(c.number_trek))<>''

    --добавление заголовка УКАЗАНИЕ [У] в переходах     
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,number_trek)
   SELECT 9,2,25,'УКАЗАНИЕ [У] '
,c.id_operation,c.number_operation,0,c.number_trek
   FROM  trekFT c
   LEFT JOIN #Temp d on c.id_number_trek=d.id_number_trek
   WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 and (ST='У' or ST=' У')

   --добавление заголовка ОСНАСТКА [Т] в переходах   
  INSERT	#treeoper(position,Level,Tip,NameChapter,id_operation,number_operation,pornom,number_trek)
   SELECT DISTINCT 10,2,18,'ОСНАСТКА [Т]'
,c.id_operation,c.number_operation,0,c.number_trek
   FROM  prop c
   LEFT JOIN #Temp d on c.id_number_trek=d.id_number_trek
   WHERE  @NOM_MAS=c.NOM_MAS AND c.number_operation=0 and ltrim(rtrim(c.number_trek))<>''

  --вывод данных
SELECT Level,Tip,NameChapter,id_operation,id_number_trek,number_trek,pornom from #treeoper 
--WHERE id_number_trek<>null
order by number_operation,number_trek,position,tip,level
--удаление временных таблиц
DROP TABLE #treeoper
DROP TABLE #Temp
END
