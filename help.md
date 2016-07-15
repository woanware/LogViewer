# Help (LogViewer)

The following provides a brief help guide for the core operations within LogViewer.


## Opening a file

Either use the File->Open menu item or drag and drop a file onto the list

## Search

- Enter the search term/pattern in the toolbar text box
- Select the Search Type from the drop down
- If you want the search to be performed on the lines currently displayed then ensure the **Cumulative** button is checked. If the button is not checked then the entire file will be searched, any existing search terms will also be removed
- Next click the **Search** button (magnify glass icon)

### Search Types

There are four different search types available:

- Sub String Case Insensitive (Default)
- Sub String Case Sensitive
- Regex Case Insensitive
- Regex Case Sensitive

### Search Terms

By using the **Cumulative** search facility, there can be multiple search terms. Each of the terms can be enabled/disabled individually. The search terms can be accessed via the list context menu. When the **Search Terms** window is displayed, check or uncheck the search terms required, upon exiting the display will be refreshed.

### Highlight Colour

The search match highlight colour can be modified via the list context menu (Search->Colour). Currently this is not persisted.

## Filtering

There are two modes for filtering; hide matched and show matched. Filtering and filter clearing is accessed via the list context menu.

- **Show matched**: Hides all lines where there is not a search match; therefore only show the matched lines
- **Hide matched**: Hides the lines that matched the search; therefore only show the lines that don't match

## Export

The export functionality exports the current view, so any lines currently visible will be exported. This functionality is accessed via the list content menu.

## Copy Line

The selected line's contents can be copied to the clipboard via the list context menu.


