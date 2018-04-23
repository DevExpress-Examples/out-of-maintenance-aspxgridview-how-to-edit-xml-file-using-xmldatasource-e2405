#region Using
using System;
using System.Collections;
using System.IO;
using System.Xml;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.WebControls;
#endregion

public partial class _Default : System.Web.UI.Page {
    protected readonly static object lockObject = new object();
    protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        e.Editor.ReadOnly = false;
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        if (e.Parameters == "Restore") {
            ASPxGridView grid = (ASPxGridView)sender;
            RestoreXMLFile(grid);
        }
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) {
        throw new InvalidOperationException("Data modifications are not allowed in online demos");

        int maxCategoryID = 0;

        foreach (XmlNode selectedNode in XmlDataSource1.GetXmlDocument().SelectNodes("Categories/Category")) {
            int categoryID = int.Parse(selectedNode.Attributes["CategoryID"].Value);
            if (categoryID > maxCategoryID)
                maxCategoryID = categoryID;
        }

        e.NewValues.Insert(0, "CategoryID", maxCategoryID + 1);

        XmlNode node = XmlDataSource1.GetXmlDocument().CreateElement("Category");
        foreach (DictionaryEntry entry in e.NewValues) {
            XmlAttribute attribute = XmlDataSource1.GetXmlDocument().CreateAttribute(entry.Key.ToString());
            attribute.Value = GetValue(entry.Value);
            node.Attributes.Append(attribute);
        }
        XmlDataSource1.GetXmlDocument().SelectSingleNode("Categories").AppendChild(node);
        SaveXml(XmlDataSource1);

        e.Cancel = true;

        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
        throw new InvalidOperationException("Data modifications are not allowed in online demos");

        ASPxGridView grid = (ASPxGridView)sender;

        int categoryID = int.Parse(e.Keys["CategoryID"].ToString());

        XmlNode node = XmlDataSource1.GetXmlDocument().SelectSingleNode(string.Format("Categories/Category[@CategoryID='{0}']", categoryID));
        if (node != null) {
            foreach (DictionaryEntry entry in e.NewValues)
                node.Attributes[entry.Key.ToString()].Value = entry.Value.ToString();

            SaveXml(XmlDataSource1);
        } else
            grid.JSProperties["cpException"] = string.Format("The DataRow with keyValue = '{0}' was deleted by another user.", categoryID);

        e.Cancel = true;

        grid.CancelEdit();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) {
        throw new InvalidOperationException("Data modifications are not allowed in online demos");

        int categoryID = int.Parse(e.Keys["CategoryID"].ToString());

        XmlNode node = XmlDataSource1.GetXmlDocument().SelectSingleNode(string.Format("Categories/Category[@CategoryID='{0}']", categoryID));
        XmlNode parent = node.ParentNode;
        parent.RemoveChild(node);

        SaveXml(XmlDataSource1);

        e.Cancel = true;

        ASPxGridView grid = (ASPxGridView)sender;
        grid.CancelEdit();
    }
    private string GetValue(object obj) {
        return obj == null ? string.Empty : obj.ToString();
    }
    protected void SaveXml(XmlDataSource xmlDataSource) {
        lock (lockObject) {
            xmlDataSource.Save();
        }
    }
    private void RestoreXMLFile(ASPxGridView grid) {
        File.Copy(
            Server.MapPath("~/App_Data/CategoriesBackUp.xml"),
            Server.MapPath("~/App_Data/Categories.xml"),
            true);
        grid.DataBind();
    }
}