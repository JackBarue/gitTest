using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace StRttmy.Common
{
    public enum SelectedStatus
    {
        Selected,
        NoSelected,
        Indeterminate
    }

    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        public bool Enabled { get; set; }

        public override object Clone()
        {
            DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        public DataGridViewDisableCheckBoxCell()
        {
            this.Enabled = true;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
        DataGridViewElementStates elementState, object value, object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
            {
                SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                graphics.FillRectangle(cellBackground, cellBounds);
                cellBackground.Dispose();
            }

            if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
            {
                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }

            RadioButtonState state = value != null && (SelectedStatus)value == SelectedStatus.Selected ? RadioButtonState.CheckedNormal : RadioButtonState.UncheckedNormal;
            Size size = RadioButtonRenderer.GetGlyphSize(graphics, state);
            Point center = new Point(cellBounds.X, cellBounds.Y);
            center.X += (cellBounds.Width - size.Width) / 2;
            center.Y += (cellBounds.Height - size.Height) / 2;

            RadioButtonRenderer.DrawRadioButton(graphics, center, state);
        }
    }

    public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public DataGridViewDisableCheckBoxColumn()
        {
            this.CellTemplate = new DataGridViewDisableCheckBoxCell();
        }
    }
}
