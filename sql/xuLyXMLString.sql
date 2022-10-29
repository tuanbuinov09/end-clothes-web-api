DECLARE @XML_STR NVARCHAR(MAX) = '<?xml version="1.0"?>
<ArrayOfHINH_ANH_SAN_PHAM_ENTITY xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <HINH_ANH_SAN_PHAM_ENTITY>
    <MA_MAU>M01</MA_MAU>
    <HINH_ANH>images3.jpg</HINH_ANH>
  </HINH_ANH_SAN_PHAM_ENTITY>
</ArrayOfHINH_ANH_SAN_PHAM_ENTITY>'

DECLARE @XML XML = CAST(@XML_STR AS XML) ;

DECLARE @idoc int
--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @idoc OUTPUT, @XML;
-- Execute a SELECT statement that uses the OPENXML rowset provider.
--SELECT    *
--FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
--            WITH (
--				MA_MAU		VARCHAR(15),
--				HINH_ANH	NVARCHAR(400)
--			);

DECLARE @MA_MAU VARCHAR(15), @HINH_ANH NVARCHAR(400)

DECLARE cur CURSOR FOR
	SELECT    *
	FROM OPENXML (@idoc, '/ArrayOfHINH_ANH_SAN_PHAM_ENTITY/HINH_ANH_SAN_PHAM_ENTITY',2)
            WITH (
				MA_MAU		VARCHAR(15),
				HINH_ANH	NVARCHAR(400)
			);

OPEN cur

FETCH NEXT FROM cur
INTO @MA_MAU, @HINH_ANH

WHILE @@FETCH_STATUS = 0
BEGIN
	--INSERT INTO @employee VALUES (@id, @name, @salary);
	PRINT(@MA_MAU + ', ' + @HINH_ANH)
	FETCH NEXT FROM cur
	INTO @MA_MAU, @HINH_ANH
END

CLOSE cur;
DEALLOCATE cur;

DECLARE @XML_STR_2 NVARCHAR(MAX) = '<?xml version="1.0"?>
<ArrayOfCHI_TIET_SAN_PHAM_ENTITY xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <CHI_TIET_SAN_PHAM_ENTITY>
    <ID_GH>0</ID_GH>
    <MA_CT_SP>0</MA_CT_SP>
    <MA_SIZE>S01</MA_SIZE>
    <MA_MAU>M01</MA_MAU>
    <GIA xsi:nil="true" />
    <SO_LUONG>0</SO_LUONG>
    <SL_TON xsi:nil="true" />
  </CHI_TIET_SAN_PHAM_ENTITY>
  <CHI_TIET_SAN_PHAM_ENTITY>
    <ID_GH>0</ID_GH>
    <MA_CT_SP>0</MA_CT_SP>
    <MA_SIZE>S03</MA_SIZE>
    <MA_MAU>M01</MA_MAU>
    <GIA xsi:nil="true" />
    <SO_LUONG>0</SO_LUONG>
    <SL_TON xsi:nil="true" />
  </CHI_TIET_SAN_PHAM_ENTITY>
  <CHI_TIET_SAN_PHAM_ENTITY>
    <ID_GH>0</ID_GH>
    <MA_CT_SP>0</MA_CT_SP>
    <MA_SIZE>S02</MA_SIZE>
    <MA_MAU>M01</MA_MAU>
    <GIA xsi:nil="true" />
    <SO_LUONG>0</SO_LUONG>
    <SL_TON xsi:nil="true" />
  </CHI_TIET_SAN_PHAM_ENTITY>
</ArrayOfCHI_TIET_SAN_PHAM_ENTITY>'