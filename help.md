# Help

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

### Multi-String Search

To search for multiple strings in one search, use the Tools->Multi-String Search menu. Select the search type, then use the import button to import the search patterns. Any previous search results are cleared when using the multi-string search. 

### Highlight Line Colour

The search match highlight colour can be modified via the list context menu (Search->Colour->Match). The colour is persisted to the configuration file.

### Context Line Colour

If the **Show context lines** option is enabled, then the context line colour can be modified via the list context menu (Search->Colour->Context). The colour is persisted to the configuration file.

## Filtering

There are two modes for filtering; hide matched and show matched. Filtering and filter clearing is accessed via the list context menu.

- **Show matched**: Hides all lines where there is not a search match; therefore only show the matched lines
- **Hide matched**: Hides the lines that matched the search; therefore only show the lines that don't match

## Export

The export functionality exports all lines within the current view or the selected lines. There is a maximum limit of 10000 lines. This functionality is accessed via the list content menu.

## Copy Line

The selected line's contents can be copied to the clipboard via the list context menu. There is a maximum limit of 10000 lines

## Context Lines

To show context around a match, the searches can be configured (Tools->Configuration) to show a user configurable number of lines before and after a search match.


