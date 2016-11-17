# History

# v0.0.9

- Fixed the missing horizontal scrollbar

# v0.0.8

- Added new window to use the contents of a full line. Double click or use Enter on a selected line.
- Added error handling for locked files
- Improved general handling when files are subsequently imported after an already loaded file

# v0.0.7

- Show the loaded file path in the Title bar (Thanks TomY)

# v0.0.6

- Added functionality to go to a specific line (Thanks TomY)
- Added functionality to go to first and last lines
- When clearing the filter, the selected row is remembered and kept selected if the filtering is cleared (Thanks TomY)

# v0.0.5

- Added functionality to show a configurable number of lines before and after a search hit (Thanks TomY)

# v0.0.4

- Added multi-string search
- Very minor tweaks

# v0.0.3

- Modified copy operation to allow multi line select
- Modified export operation to allow exporting of multi selected lines
- Imposed an arbitrary limit of 10000 lines for the multi select copy and export operations  
- Added user selection of highlight colour
- Added help.md
- Compiled PDF versions of markdown documents
- Included [Nett](https://github.com/paiden/Nett) library for [TOML](https://github.com/toml-lang/toml) support
- Added configuration persistence of highlight colour and multi select limit, which uses TOML format
- A few bug fixes

# v0.0.2

- Added drag and drop file loading
- Copy line contents to clipboard (via context menu)
- Added status messages
- Added operation timing

# v0.0.1

- Initial release