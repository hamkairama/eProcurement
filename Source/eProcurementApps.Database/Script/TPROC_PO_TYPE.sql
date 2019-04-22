insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('PI', 'Promo item non stock', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'Promotional Item'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('HS', 'Hardware soft ware', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'IT'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('ES', 'for event and sales conf', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'EVENT & SALES CONFERENCE'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('MA', 'Marketing Allowance', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'MARKETING ALLOWANCE'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('AS', 'for asset', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'Asset'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('PN', 'for printing', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'Printing'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('OS', 'for office supplies', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'Office Supplies'));

insert into tproc_po_type (PO_TYPE_NAME, PO_TYPE_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS, FORM_TYPE_ID)
values ('NA', 'for non asset', SYSDATETIME(), 'System', null, null, 0, (select id from tproc_form_type where FORM_TYPE_NAME = 'Non Asset'));




--select * from tproc_po_type;
--delete tproc_po_type;