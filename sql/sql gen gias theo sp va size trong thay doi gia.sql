-- chuooix caau lenh them gia cho cac chi tiet san pham chua co trong bang thay doi gia vua cap nhat them mau sac

INSERT INTO THAY_DOI_GIA (GIA, MA_CT_SP, MA_NV, NGAY_THAY_DOI) 
SELECT (SELECT TOP (1) GIA FROM THAY_DOI_GIA tdg --top1 do co ctsp co 2 lan thay doi
WHERE ((SELECT MA_SIZE FROM CHI_TIET_SAN_PHAM WHERE MA_CT_SP = tdg.MA_CT_SP) = ctsp.MA_SIZE) 
AND ((SELECT MA_SP FROM CHI_TIET_SAN_PHAM WHERE MA_CT_SP = tdg.MA_CT_SP) = ctsp.MA_SP)) AS GIA
, ctsp.MA_CT_SP, 
'NV01' AS MA_NV, GETDATE() AS NGAY_THAY_DOI 
FROM 
CHI_TIET_SAN_PHAM ctsp WHERE ctsp.MA_CT_SP NOT IN (SELECT MA_CT_SP FROM THAY_DOI_GIA)



