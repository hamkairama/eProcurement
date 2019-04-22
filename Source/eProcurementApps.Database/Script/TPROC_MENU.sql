-----------------------------------------------Start Parent ------------------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU01', null, 'Setup', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU21', null, 'Purchasing Request', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU56', null, 'Price Comparison', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU27', null, 'Purchase Order', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU63', null, 'Matching', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU57', null, 'CRV', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU36', null, 'Report', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU42', null, 'Daily Operation', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU39', null, 'Request Self Service', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU49', null, 'Others', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------------------------------------------------------------------------------------------------------------------


----------------------------------------------Start Declare variabel----------------------------------------------------------
declare 
@Lmenu_setup_id integer = (select id from TPROC_MENU where MENU_NAME = 'MNU01'),
@Lmenu_pr_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU21'),
@Lmenu_pricecom_id integer = (select id from TPROC_MENU where MENU_NAME = 'MNU56'),
@Lmenu_po_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU27'),
@Lmenu_matching_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU63'),
@Lmenu_crv_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU57'),
@Lmenu_report_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU36'),
@Lmenu_daily_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU42'),
@Lmenu_request_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU39'),
@Lmenu_other_id integer = (select id  from TPROC_MENU where MENU_NAME = 'MNU49')
----------------------------------------------End Declare variabel----------------------------------------------------------

-----------------------------------------------Start Ins_TPROC_MENU setup------------------------------------------------------------------------------------
begin

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU02', @Lmenu_setup_id, 'Currency', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU03', @Lmenu_setup_id, 'Vat', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU04', @Lmenu_setup_id, 'Pph', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU05', @Lmenu_setup_id, 'Related Department', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU06', @Lmenu_setup_id, 'Division', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU07', @Lmenu_setup_id, 'SUN Budget', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU08', @Lmenu_setup_id, 'Good Type', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU09', @Lmenu_setup_id, 'Form Type', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU10', @Lmenu_setup_id, 'Form Sub Type', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU11', @Lmenu_setup_id, 'Holiday', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU12', @Lmenu_setup_id, 'Department', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU13', @Lmenu_setup_id, 'Po Type', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU14', @Lmenu_setup_id, 'Delivery Address', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU15', @Lmenu_setup_id, 'Supplier', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU16', @Lmenu_setup_id, 'Role', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU17', @Lmenu_setup_id, 'User', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU18', @Lmenu_setup_id, 'Level', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ( 'MNU19', @Lmenu_setup_id, 'Work Area', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU20', @Lmenu_setup_id, 'Item', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU23', @Lmenu_setup_id, 'Chart Of Account', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU62', @Lmenu_setup_id, 'Approval', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------End Ins_TPROC_MENU setup------------------------------------------------------------------------------------




-----------------------------------------------Start Ins_TPROC_MENU PR------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU22', @Lmenu_pr_id, 'Create Purchasing Request', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU24', @Lmenu_pr_id, 'My List PR',SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU25', @Lmenu_pr_id, 'My List Approval PR WA', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU26', @Lmenu_pr_id, 'My List Approval PR RD', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU29', @Lmenu_pr_id, 'All List PR', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU30', @Lmenu_pr_id, 'PRs Ready To Handle', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU31', @Lmenu_pr_id, 'PRs Ready To Create PO', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU32', @Lmenu_pr_id, 'List PR By Submitter', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU33', @Lmenu_pr_id, 'List PR Complete', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU34', @Lmenu_pr_id, 'List PR Sign Off', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU ( MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU35', @Lmenu_pr_id, 'List PR Rejected', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU45', @Lmenu_pr_id, 'PRs Ready To Complete', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU46', @Lmenu_pr_id, 'PRs Ready To Sign Off', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU47', @Lmenu_pr_id, 'My List Ready To Sign Off', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU PR------------------------------------------------------------------------------------


-----------------------------------------------Start Ins_TPROC_MENU PriceCom------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU60', @Lmenu_pricecom_id , 'Create Comparison', SYSDATETIME(), 'System', null, '', 0);


insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU65', @Lmenu_pricecom_id , 'List PriceCom', SYSDATETIME(), 'System', null, '', 0);
----------------------------------------------end Ins_TPROC_MENU PriceCom------------------------------------------------------------------------------------


-----------------------------------------------Start Ins_TPROC_MENU PO------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU28', @Lmenu_po_id, 'Create Purchase Order', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU48', @Lmenu_po_id, 'List Purchase Order', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU PO------------------------------------------------------------------------------------



-----------------------------------------------Start Ins_TPROC_MENU Matching------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU67', @Lmenu_matching_id, 'Good Match', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU68', @Lmenu_matching_id, 'Good Invoice', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU Mathcing------------------------------------------------------------------------------------



-----------------------------------------------Start Ins_TPROC_MENU CRV------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU61', @Lmenu_crv_id, 'Create CRV', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU66', @Lmenu_crv_id, 'List CRV', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU CRV------------------------------------------------------------------------------------



-----------------------------------------------Start Ins_TPROC_MENU Report------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU37', @Lmenu_report_id, 'My List PR', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU38', @Lmenu_report_id, 'TAT', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU69', @Lmenu_report_id, 'Report Trans', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU Report------------------------------------------------------------------------------------



-----------------------------------------------Start Ins_TPROC_MENU Daily ------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU43', @Lmenu_daily_id, 'Data Stock', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU64', @Lmenu_daily_id, 'Data Stock Summary', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU44', @Lmenu_daily_id, 'Data Non Stock', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU Daily------------------------------------------------------------------------------------



-----------------------------------------------Start Ins_TPROC_MENU REquest self service--------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU40', @Lmenu_request_id, 'Requests Need Approve', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU41', @Lmenu_request_id, 'Requests Need Complete', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU51', @Lmenu_request_id, 'Request User', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU52', @Lmenu_request_id, 'Request WA', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU53', @Lmenu_request_id, 'Request RD', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU54', @Lmenu_request_id, 'Request COA', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU55', @Lmenu_request_id, 'Request Form SubType', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU58', @Lmenu_request_id, 'Requests Outstanding', SYSDATETIME(), 'System', null, '', 0);

insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU59', @Lmenu_request_id, 'Requests Completed', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU REquest self service------------------------------------------------------------------------------------



-----------------------------------------------Start Ins_TPROC_MENU Other------------------------------------------------------------------------------------
insert into TPROC_MENU (MENU_NAME, MENU_PARENT_ID, MENU_DESCRIPTION, CREATED_TIME, CREATED_BY, LAST_MODIFIED_TIME, LAST_MODIFIED_BY, ROW_STATUS)
values ('MNU50', @Lmenu_other_id, 'Documents', SYSDATETIME(), 'System', null, '', 0);
-----------------------------------------------end Ins_TPROC_MENU Other------------------------------------------------------------------------------------
end;









--select * from TPROC_MENU;
--delete TPROC_MENU;