<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
<!-- default file list end -->
# ASPxGridView - How to edit XML file using XmlDataSource


<p>The <a href="http://msdn.microsoft.com/en-us/library/system.web.ui.webcontrols.xmldatasource%28VS.80%29.aspx">XmlDataSource</a> control doesn't have the built-in API to edit an <strong>XML File</strong> specified via the XmlDataSource's <a href="http://msdn.microsoft.com/en-US/library/system.web.ui.webcontrols.xmldatasource.datafile%28v=VS.80%29.aspx">DataFile</a> property.</p><p>So, it's necessary to edit the <strong>XML File</strong> via the built-in .NET Framework's classes: XmlNode, XmlAttribute, etc.</p><p><strong>Note:</strong> As the <strong>XmlDataSource</strong> control doesn't have the built-in API to edit the <strong>XML File</strong>, the ASPxGridView control renders its editor as <strong>ReadOnly</strong>. So, it's necessary to enable the ASPxGridView's editors explicitely by handling the ASPxGridView's <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_CellEditorInitializetopic">CellEditorInitialize</a> event.</p>

<br/>


