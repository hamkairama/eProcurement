insert into TPROC_CURRENCY (CURRENCY_NAME, RATE, START_DATE, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, CONVERSION_RP)
values ('IDR', 1, SYSDATETIME(), SYSDATETIME(), 'System', null, null, 0, 1);
insert into TPROC_CURRENCY (CURRENCY_NAME, RATE, START_DATE, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, CONVERSION_RP)
values ('USD', 2, SYSDATETIME(), SYSDATETIME(), 'System', null, null, 0, 13000);


--select * from tproc_currency
--delete tproc_currency