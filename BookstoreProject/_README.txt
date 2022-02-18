
Im going to write some reference notes here that may answer some of your questions.
I may add more as I find them or y'all ask them.
-Morgen H



--- Project Updates

- I recommend you use the git push/pull wisely. If you finish a feature it will probably be a 
  good idea to push as soon as possible. Also if you haven't worked on it for a while, check 
  for updates. The icons next to the files in the solution explorer can show their update status.



--- Databases

where is the database and its tables?
-	In the Server Explorer tab (either on the left side or try searching for it) there is 
	a folder labeled Data Connections. In that is the Bookstore Database and inside that 
	is all its files and tables.
-	If the data connections folder is empty, try opening the solution explorer and clicking on
	the BookstoreDatabase under the App_Data folder. This can refresh the the connections and 
	it should be displayed properly.


	
--- Web Pages

- All webpages (besides the default page) are stored in the Webpages folder. They can be referenced
  by the path "~/Webpages/PageName".
- Each web page (or form) is listed in the solution explorer as an .aspx file. It also has two
  subfiles: its main C# code and a designer c# code (which I have never needed to open).
- The .aspx file defines how the page will look and has two GUIs: Source and Design.
  Source is used to look at the actual code that is making the page while Design is more useful
  for actually making it look pretty using drag-n-drop itmes from the toolbox tab. You can swap 
  between these by using the buttons on the bottom left corner of the screen.
- I made an ExampleForm that you can use as a guide.

New Page 
-	A new page is created by right-clicking the solution explorer then "Add..." about 
	halfway down the list. Then click either "New Item..." then "Web Form" or, more simply, 
	just "Web Form" near the bottom if the list.
-	After creating a new page, we can use the built in formatting to make them all look the same
	like having the same background, heading, and font style. This is done by adding the following
	code to the source side of the .aspx file:


	-Add this to the first line between any other tags.

			Title="Whatever the page is called" MasterPageFile="~/Site.Master"	
	 

	-Replace everything after the first line with this code. It will hold every element in the page.

			<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

			</asp:Content>

-	After these are in place, you can drag items from the toolbox panel and edit them in the 
	properties panel. **Note** Please rename every item you add for easier readbility and 
	implementation of code that calls those items.



Navigating between pages

- Using the tag PostBackURL="destination.aspx" on a button, you can link it to open the desired
  page. It is also in the button's properties selection.


Page Events

- In the properties pages of most elements is an events section (little lightning bolt at the top).
  In here you can select which function in the C# file will run on which event. You can also type
  a new function name in the box and it will create the whole method signature for you.