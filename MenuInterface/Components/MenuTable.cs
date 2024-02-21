namespace MenuInterface.Components;

public class MenuTable
{
    private readonly MenuGroup[] _groups;
    
    public MenuTable(MenuGroup[] groups)
    {
        _groups = groups;
        UpdateSelectedItem(0, 0);
        UpdateItemsLength();
        UpdateGroupsLength();
    }

    private void UpdateItemsLength()
    {
        int maxColumnCount = _groups.Select(group => group.Items.Length).Prepend(0).Max();
        for (int columnIndex = 0; columnIndex < maxColumnCount; columnIndex++)
        {
            int maxItemInColumnStringLength = (
                from @group in _groups where columnIndex < @group.Items.Length 
                select @group.Items[columnIndex].ToString().Length
                ).Prepend(0).Max();

            foreach (MenuGroup group in _groups)
            {
                if (columnIndex < group.Items.Length)
                {
                    group.Items[columnIndex].ExpectedLength = maxItemInColumnStringLength;
                }
            }
        }
    }

    private void UpdateGroupsLength()
    {
        int maxGroupStringLength = _groups.Select(group => group.ToString().Length).Prepend(0).Max();
        foreach (MenuGroup group in _groups)
        {
            group.ExpectedLength = maxGroupStringLength;
        }
    }
    
    /// <summary>
    /// Gets selected item indexes.
    /// </summary>
    /// <returns>Selected item row and column indexes.</returns>
    public (int, int) GetSelectedItemIndexes()
    {
        for (int rowIndex = 0; rowIndex < _groups.Length; rowIndex++)
        {
            MenuItem[] items = _groups[rowIndex].Items;
            for (int columnIndex = 0; columnIndex < items.Length; columnIndex++)
            {
                MenuItem item = items[columnIndex];
                if (item.Selected)
                {
                    return (rowIndex, columnIndex);
                }
            }
        }

        const int initialRowNumber = 0, initialColumnNumber = 0;
        _groups[initialRowNumber].Items[initialColumnNumber].Selected = true;
        
        return (initialRowNumber, initialColumnNumber);
    }

    public MenuItem GetSelectedItem()
    {
        (int rowIndex, int columnIndex) = GetSelectedItemIndexes();
        return this[rowIndex][columnIndex];
    }
    
    /// <summary>
    /// Updates the currently selected item.
    /// </summary>
    public void UpdateSelectedItem(int updatedRow, int updatedColumn)
    {
        (int currentRow, int currentColumn) = GetSelectedItemIndexes();
        
        MenuItem[] rowItems = _groups[currentRow].Items;
        
        // Check accessible to change selected item.
        if (
            currentRow == updatedRow && currentColumn == updatedColumn
            || updatedColumn >= rowItems.Length || updatedColumn < 0
            || updatedRow >= _groups.Length || updatedRow < 0
        )
        {
            return;
        }

        if (updatedRow != currentRow)
        {
            // Update next row selected column.
            MenuItem[] nextRowItems = _groups[updatedRow].Items;
            if (updatedColumn >= nextRowItems.Length)
            {
                updatedColumn = nextRowItems.Length - 1;
            }
        }

        rowItems[currentColumn].Selected = false;
        _groups[updatedRow].Items[updatedColumn].Selected = true;
    }

    private MenuGroup this[int index] => _groups[index];

    public void Write()
    {
        foreach (MenuGroup group in _groups)
        {
            group.Write();
        }
    }
}