using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinaimgPublisher.Extension
{
    public static class Basic
    {
        public static bool In<T>(this T obj, params T[] args)
        {
            return args.Contains(obj);
        }

        public static void DataGridViewSortRows(DataGridView dgv, DataGridViewColumn sortColumn, bool orderToggle)
        {
            try
            {
                if (sortColumn == null)
                    return;
                if (sortColumn.SortMode == DataGridViewColumnSortMode.Programmatic &&
                    dgv.SortedColumn != null && !dgv.SortedColumn.Equals(sortColumn))
                {
                    dgv.SortedColumn.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }

                ListSortDirection sortDirection;
                if (orderToggle)
                {
                    sortDirection = dgv.SortOrder == System.Windows.Forms.SortOrder.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                }
                else
                {
                    sortDirection = dgv.SortOrder == System.Windows.Forms.SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                }
                System.Windows.Forms.SortOrder sortOrder =
                    sortDirection == ListSortDirection.Ascending ? System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                dgv.Sort(sortColumn, sortDirection);
                if (sortColumn.SortMode == DataGridViewColumnSortMode.Programmatic)
                {
                    sortColumn.HeaderCell.SortGlyphDirection = sortOrder;
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
