#Region "Using"
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.IO
Imports System.Xml
Imports DevExpress.Web
Imports System.Web.UI.WebControls
#End Region

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected ReadOnly Shared lockObject As Object = New Object()
	Protected Sub ASPxGridView1_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		e.Editor.ReadOnly = False
	End Sub
	Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		If e.Parameters = "Restore" Then
			Dim grid As ASPxGridView = CType(sender, ASPxGridView)
			RestoreXMLFile(grid)
		End If
	End Sub
	Protected Sub ASPxGridView1_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
                            Throw New InvalidOperationException("Data modifications are not allowed in online demos")

		Dim maxCategoryID As Integer = 0

		For Each selectedNode As XmlNode In XmlDataSource1.GetXmlDocument().SelectNodes("Categories/Category")
			Dim categoryID As Integer = Integer.Parse(selectedNode.Attributes("CategoryID").Value)
			If categoryID > maxCategoryID Then
				maxCategoryID = categoryID
			End If
		Next selectedNode

		e.NewValues.Insert(0, "CategoryID", maxCategoryID + 1)

		Dim node As XmlNode = XmlDataSource1.GetXmlDocument().CreateElement("Category")
		For Each entry As DictionaryEntry In e.NewValues
			Dim attribute As XmlAttribute = XmlDataSource1.GetXmlDocument().CreateAttribute(entry.Key.ToString())
			attribute.Value = GetValue(entry.Value)
			node.Attributes.Append(attribute)
		Next entry
		XmlDataSource1.GetXmlDocument().SelectSingleNode("Categories").AppendChild(node)
		SaveXml(XmlDataSource1)

		e.Cancel = True

		Dim grid As ASPxGridView = CType(sender, ASPxGridView)
		grid.CancelEdit()
	End Sub
	Protected Sub ASPxGridView1_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
                            Throw New InvalidOperationException("Data modifications are not allowed in online demos")

		Dim grid As ASPxGridView = CType(sender, ASPxGridView)

		Dim categoryID As Integer = Integer.Parse(e.Keys("CategoryID").ToString())

		Dim node As XmlNode = XmlDataSource1.GetXmlDocument().SelectSingleNode(String.Format("Categories/Category[@CategoryID='{0}']", categoryID))
		If node IsNot Nothing Then
			For Each entry As DictionaryEntry In e.NewValues
				node.Attributes(entry.Key.ToString()).Value = entry.Value.ToString()
			Next entry

			SaveXml(XmlDataSource1)
		Else
			grid.JSProperties("cpException") = String.Format("The DataRow with keyValue = '{0}' was deleted by another user.", categoryID)
		End If

		e.Cancel = True

		grid.CancelEdit()
	End Sub
	Protected Sub ASPxGridView1_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
                            Throw New InvalidOperationException("Data modifications are not allowed in online demos")

		Dim categoryID As Integer = Integer.Parse(e.Keys("CategoryID").ToString())

		Dim node As XmlNode = XmlDataSource1.GetXmlDocument().SelectSingleNode(String.Format("Categories/Category[@CategoryID='{0}']", categoryID))
		Dim parent As XmlNode = node.ParentNode
		parent.RemoveChild(node)

		SaveXml(XmlDataSource1)

		e.Cancel = True

		Dim grid As ASPxGridView = CType(sender, ASPxGridView)
		grid.CancelEdit()
	End Sub
	Private Function GetValue(ByVal obj As Object) As String
		Return If(obj Is Nothing, String.Empty, obj.ToString())
	End Function
	Protected Sub SaveXml(ByVal xmlDataSource As XmlDataSource)
		SyncLock lockObject
			xmlDataSource.Save()
		End SyncLock
	End Sub
	Private Sub RestoreXMLFile(ByVal grid As ASPxGridView)
		File.Copy(Server.MapPath("~/App_Data/CategoriesBackUp.xml"), Server.MapPath("~/App_Data/Categories.xml"), True)
		grid.DataBind()
	End Sub
End Class