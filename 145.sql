USE [PCTEXT]
GO
/****** Object:  StoredProcedure [dbo].[e012p145_GetTipTechnicalArrays]    Script Date: 06.10.2023 14:19:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Процедура получения списков из тезнологических массивов
-- Автор: Перевозчиков Андрей Иванович 556176
-- Дата создания: 07.09.2023 
-- Входные параметры: 
-- @NOM_MAS smallint  номер массива (для @TIP=12 и -0)
-- @TIP smallint  тип операции
-- @cex smallint цех (только для @TIP, в остальных случаях 0)
-- =============================================
ALTER   PROCEDURE [dbo].[e012p145_GetTipTechnicalArrays]
	@NOM_MAS smallint,
	@TIP smallint,
	@cex varchar(3)  
AS
BEGIN
	/*declare 
@NOM_MAS smallint ,
@TIP smallint

set @NOM_MAS=2
set @TIP=8
*/
--ТТ
if(@TIP=1)
BEGIN
	SELECT TTTB,komm from PCTEXT.[dbo].SPR_TTTB
	WHERE nom_mas=@nom_mas and substring(tttb,1,1)='1'
END

--ТТБ
if(@TIP=2)
BEGIN
	SELECT TTTB,komm from PCTEXT.[dbo].SPR_TTTB
	WHERE nom_mas=@nom_mas and substring(tttb,1,1)='2'
END

--ТЭБ
if(@TIP=3)
BEGIN
	SELECT TTTB,komm from PCTEXT.[dbo].SPR_TTTB
	WHERE nom_mas=@nom_mas and substring(tttb,1,1)='3'
END

--ОПЕРАЦИИ [A] технологические массивы
if(@TIP=4)
BEGIN 
 SELECT NOM_CEH,KOD_OPER,nop as NMOPR,OBDOK,NOM_TI,Komm
  FROM  SPR_OPER a
  join INFORM.DBO.KTO  b on b.KOP=a.KOD_OPER
  WHERE nom_mas=@nom_mas
 ORDER BY NMOPR
END

--ОПЕРАЦИИ [A] справочник операций
if(@TIP=12)
begin
 select kop,nop from  INFORM.DBO.KTO 
  order by nop
END

--ОБОРУДОВАНИЕ [Б] fullname-- наименование профессии,cex - цех, в котором есть эта профессия
if(@TIP=5)
BEGIN
  if @cex<>'*'
	SELECT  distinct KOD,NST,a.kprof,fullname,Komm,str(b.cex,3) as cex     
	  FROM  SPR_eqipment a
	  join rc..t056t00_sprofcex b on ltrim(rtrim(a.kprof))='1'+str(b.kprof,4) 
	  join inform..sprof c on b.kprof=c.kr 
	  where b.cex=@cex  and a.nom_mas=@nom_mas
	union
	SELECT  distinct KOD,NST,kprof,'' as fullname,Komm,'' as cex     
	  FROM  SPR_eqipment a1     
	  where nom_mas=@nom_mas and 
	  not exists(select kprof from rc..t056t00_sprofcex b1 where  ltrim(rtrim(a1.kprof))='1'+str(b1.kprof,4) and b1.cex=@cex) 
	  order by  kod,nst,kprof
   else
    SELECT  distinct nom_mas,KOD,NST,kprof,'' as fullname,Komm,'*' as cex     
     FROM  SPR_eqipment 
     where nom_mas=@nom_mas
	 order by  kod,nst,kprof
END

--ОБОРУДОВАНИЕ [Б] выбор кодов профессий из справочника профессий
if(@TIP=13)
BEGIN
 if @cex<>'*'
	select cex,'1'+str(kprof,4)as kprof,fullname
     from rc..t056t00_sprofcex  a
     join inform..sprof b on a.kprof=b.kr 
     where cex=@cex
     order by kprof
  else
   select cex,'1'+str(kprof,4)as kprof,fullname
    from rc..t056t00_sprofcex  a
    join inform..sprof b on a.kprof=b.kr 
    order by cex,kprof
END


--МАТЕРИАЛЫ [М]
if(@TIP=6)
BEGIN
  SELECT naimc=ltrim(rtrim(case when b.id_nn_material is null then b.naimc else b1.nd end)),
      EI,VR,EN,KRNAIMC,komm,id_nn_material,Concetration,Viscosity,Density	
	  from  SPR_MATER b	   
	  left join  kltmcm b1 on b1.id_gpf=b.id_nn_material  
	WHERE nom_mas=@nom_mas
	ORDER BY naimc
END

--МАТЕРИАЛЫ [М] справочник единиц измерения
if(@TIP=14)
 select DISTINCT ei from  sprei order by ei

--ОСНАСТКА [Т]
if(@TIP=7)
BEGIN	
  SELECT NAIMCTO,SHC,PR,pr_name=(case when pr=1 then 'ПРИСПОСОБЛЕНИЯ'
									  when pr=2 then 'ВСПОМОГАТЕЛЬНЫЙ ИНСТРУМЕНТ'
									  when pr=3 then 'РЕЖУЩИЙ ИНСТРУМЕНТ'
									  when pr=4 then 'СЛЕСАРНО-МОНТАЖНЫЙ ИНСТРУМЕНТ'
									  when pr=5 then 'СПЕЦИАЛЬНЫЙ ИНСТРУМЕНТ'
									  when pr=6 then 'МЕРИТЕЛЬНЫЙ ИНСТРУМЕНТ'
					                  else 'ПРИСПОСОБЛЕНИЯ' end),Komm
  FROM  SPR_OSN
  WHERE nom_mas=@nom_mas
	ORDER BY NAIMCTO,SHC

END

--ПЕРЕХОДЫ [О]
if(@TIP=8)
BEGIN
	SELECT information,Prizn from PCTEXT.[dbo].SPR_PERE
	WHERE nom_mas=@nom_mas and substring(information,1,1)='О'
	ORDER BY information
END

--РЕЖИМЫ [Р]
if(@TIP=9)
BEGIN
	SELECT information,Prizn,P,t,pH,OPA_K,D,U,Time
	from PCTEXT.[dbo].SPR_PERE
	WHERE nom_mas=@nom_mas and substring(information,1,1)='Р'
	ORDER BY information
END

--ВНИМАНИЕ [В]
if(@TIP=10)
BEGIN
	SELECT information,Prizn from PCTEXT.[dbo].SPR_PERE
	WHERE nom_mas=@nom_mas and substring(information,1,1)='В'
	ORDER BY information
END

--УКАЗАНИЕ [У]
if(@TIP=11)
BEGIN
	SELECT information,Prizn from PCTEXT.[dbo].SPR_PERE
	WHERE nom_mas=@nom_mas and substring(information,1,1)='У'
	ORDER BY information
END


END
