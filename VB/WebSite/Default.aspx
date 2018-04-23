<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
	<script language="javascript" type="text/javascript">
		function OnEndCallbackEventHandler(s, e) {
			if (s.cpException) {
				alert(s.cpException.toString());
				delete s.cpException;
			}
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"
			ClientInstanceName="grid" DataSourceID="XmlDataSource1" KeyFieldName="CategoryID"
			OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize" OnCustomCallback="ASPxGridView1_CustomCallback"
			OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating"
			OnRowDeleting="ASPxGridView1_RowDeleting">
			<Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowDeleteButton="True"/>
				<dx:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="1">
					<EditFormSettings Visible="False" />
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="2">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3">
				</dx:GridViewDataTextColumn>
			</Columns>
			<Settings ShowStatusBar="Visible" />
			<Templates>
				<StatusBar>
					<table cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td align="left">
								<dx:ASPxButton ID="ASPxButton1" runat="server" Text="Add New Record" AutoPostBack="false">
									<ClientSideEvents Click="function(s, e) { grid.AddNewRow(); }" />
								</dx:ASPxButton>
							</td>
							<td align="right">
								<dx:ASPxButton ID="ASPxButton2" runat="server" Text="Restore XML File" AutoPostBack="false">
									<ClientSideEvents Click="function(s, e) { grid.PerformCallback('Restore'); }" />
								</dx:ASPxButton>
							</td>
						</tr>
					</table>
				</StatusBar>
			</Templates>
			<ClientSideEvents EndCallback="OnEndCallbackEventHandler" />
		</dx:ASPxGridView>
	</div>
	<asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/Categories.xml">
	</asp:XmlDataSource>
	</form>
</body>
</html>