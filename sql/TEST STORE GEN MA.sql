DECLARE @result varchar(15)

EXEC GEN_CODE 'SAN_PHAM', 'SP', @result OUTPUT

SELECT @result, LEN(@result)

EXEC GEN_CODE 'SAN_PHAM', 'SP', @result OUTPUT

SELECT @result, LEN(@result)

DECLARE @MA_SP_MOI VARCHAR(15)
		EXEC GEN_CODE @tableName = 'SAN_PHAM', @prefix = 'SP', @result = @MA_SP_MOI OUTPUT

		SELECT @MA_SP_MOI, LEN(@MA_SP_MOI)