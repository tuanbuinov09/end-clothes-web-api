USE [CLOTHING_STORE]
GO
/****** Object:  StoredProcedure [dbo].[THEM_GIO_HANG]    Script Date: 29/10/2022 5:55:49 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[THEM_SAN_PHAM]
	 @TEN_SP nvarchar(150)
	,@MA_TL VARCHAR(15)
	,@HINH_ANH nvarchar(400)
	,@MO_TA NVARCHAR(500) = NULL
	,@MA_NV VARCHAR(15)
	,@xml_LIST_HINH_ANH_SP_STR NVARCHAR(MAX)
	,@xml_LIST_CHI_TIET_SP_STR NVARCHAR(MAX)
AS
BEGIN
	BEGIN TRANSACTION
		DECLARE @MA_SP_MOI VARCHAR(15)
		EXEC GEN_CODE @tableName = 'SAN_PHAM', @prefix = 'SP', @result = @MA_SP_MOI OUTPUT

		
		-- thêm vào bảng sản phẩm
			INSERT INTO SAN_PHAM(MA_SP ,TEN_SP, MA_TL, NGAY_TAO, LUOT_XEM, HINH_ANH, MO_TA)
				VALUES(@MA_SP_MOI, @TEN_SP, @MA_TL, GETDATE(), 0, @HINH_ANH, @MO_TA)
		
				IF @@ERROR <> 0
				GOTO ABORT

		-- xử lý xml list hình ảnh sản phẩm
		DECLARE @XML_LIST_HINH_ANH_SP XML = CAST(@xml_LIST_HINH_ANH_SP_STR AS XML) ;

		DECLARE @idoc int
		--Create an internal representation of the XML document.
		EXEC sp_xml_preparedocument @idoc OUTPUT, @XML_LIST_HINH_ANH_SP;
		-- Execute a SELECT statement that uses the OPENXML rowset provider.
		--SELECT    *
		--FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
		--            WITH (
		--				MA_MAU		VARCHAR(15),
		--				HINH_ANH	NVARCHAR(400)
		--			);

		DECLARE @x_MA_MAU VARCHAR(15), @x_HINH_ANH NVARCHAR(400)
		DECLARE cur CURSOR FOR
			SELECT    *
				FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
					WITH (
						MA_MAU		VARCHAR(15),
						HINH_ANH	NVARCHAR(400)
					);
		OPEN cur

		FETCH NEXT FROM cur
		INTO @x_MA_MAU, @x_HINH_ANH

		WHILE @@FETCH_STATUS = 0
		BEGIN

			INSERT INTO HINH_ANH_SAN_PHAM(MA_SP, MA_MAU, HINH_ANH) 
				VALUES (@MA_SP_MOI, @x_MA_MAU, @x_HINH_ANH);
			IF @@ERROR <> 0
				GOTO ABORT

			PRINT(@x_MA_MAU + ', ' + @x_HINH_ANH)
			FETCH NEXT FROM cur
			INTO @x_MA_MAU, @x_HINH_ANH
		END

		CLOSE cur;
		DEALLOCATE cur;
		--end xử lý xml list hinh anh san pham

		-- xử lý xml list chi tiết sản phẩm
		DECLARE @XML_LIST_CHI_TIET_SP XML = CAST(@xml_LIST_CHI_TIET_SP_STR AS XML) ;

		DECLARE @idoc_CTSP int
		--Create an internal representation of the XML document.
		EXEC sp_xml_preparedocument @idoc_CTSP OUTPUT, @XML_LIST_CHI_TIET_SP;
		-- Execute a SELECT statement that uses the OPENXML rowset provider.
		--SELECT    *
		--FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
		--            WITH (
		--				MA_MAU		VARCHAR(15),
		--				HINH_ANH	NVARCHAR(400)
		--			);

		DECLARE @ctsp_MA_MAU VARCHAR(15), @ctsp_MA_SIZE VARCHAR(15)

		DECLARE cur_CTSP CURSOR FOR
			SELECT *
			FROM OPENXML (@idoc_CTSP, '/ArrayOfCHI_TIET_SAN_PHAM_ENTITY/CHI_TIET_SAN_PHAM_ENTITY',2)
					WITH (
						MA_MAU	VARCHAR(15),
						MA_SIZE	VARCHAR(15)
					);

		OPEN cur_CTSP

		FETCH NEXT FROM cur_CTSP
		INTO @ctsp_MA_MAU, @ctsp_MA_SIZE

		WHILE @@FETCH_STATUS = 0
		BEGIN
			INSERT INTO CHI_TIET_SAN_PHAM(MA_SP, MA_MAU, MA_SIZE, SL_TON) 
				VALUES (@MA_SP_MOI, @ctsp_MA_MAU, @ctsp_MA_SIZE, 0);
			IF @@ERROR <> 0
				GOTO ABORT
			PRINT(@ctsp_MA_MAU + ', ' + @ctsp_MA_SIZE)
			FETCH NEXT FROM cur_CTSP
			INTO @ctsp_MA_MAU, @ctsp_MA_SIZE
		END

		CLOSE cur_CTSP;
		DEALLOCATE cur_CTSP;
		--end xử lý xml list chi tiet san pham

		INSERT INTO THAY_DOI_GIA (MA_CT_SP, GIA, NGAY_THAY_DOI, MA_NV) 
			SELECT MA_CT_SP, 0 AS GIA, GETDATE() AS NGAY_THAY_DOI, @MA_NV AS MA_NV FROM CHI_TIET_SAN_PHAM WHERE MA_SP = @MA_SP_MOI
		IF @@ERROR <> 0
				GOTO ABORT

	COMMIT TRANSACTION

	SELECT @MA_SP_MOI AS affectedId, '' AS errorDesc, 'OK' AS responseMessage 
	ABORT:
		ROLLBACK TRANSACTION
		CLOSE cur;
		DEALLOCATE cur;
		CLOSE cur_CTSP;
		DEALLOCATE cur_CTSP;
		SELECT '' AS affectedId, @@ERROR AS errorDesc, 'Có lỗi xảy ra' AS responseMessage 
END

--DECLARE @XML_STR NVARCHAR(MAX) = '<?xml version="1.0"?>
--<ArrayOfHINH_ANH_SAN_PHAM_ENTITY xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
--  <HINH_ANH_SAN_PHAM_ENTITY>
--    <MA_MAU>M01</MA_MAU>
--    <HINH_ANH>images3.jpg</HINH_ANH>
--  </HINH_ANH_SAN_PHAM_ENTITY>
--</ArrayOfHINH_ANH_SAN_PHAM_ENTITY>'

--DECLARE @XML XML = CAST(@XML_STR AS XML) ;

--DECLARE @tempTable TABLE
--(
--	MA_MAU		VARCHAR(15),
--	HINH_ANH	NVARCHAR(400)
--)
--DECLARE @idoc int
----Create an internal representation of the XML document.
--EXEC sp_xml_preparedocument @idoc OUTPUT, @XML;
---- Execute a SELECT statement that uses the OPENXML rowset provider.
----SELECT    *
----FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
----            WITH (
----				MA_MAU		VARCHAR(15),
----				HINH_ANH	NVARCHAR(400)
----			);

--DECLARE @MA_MAU VARCHAR(15), @HINH_ANH NVARCHAR(400)

--DECLARE cur CURSOR FOR
--	SELECT    *
--	FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
--            WITH (
--				MA_MAU		VARCHAR(15),
--				HINH_ANH	NVARCHAR(400)
--			);

--OPEN cur