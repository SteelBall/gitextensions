﻿using System.Drawing;
using System.Windows.Forms;
using GitCommands;
using GitExtUtils.GitUI;

namespace GitUI.UserControls.RevisionGrid.Columns
{
    internal sealed class AuthorColumnProvider : ColumnProvider
    {
        private readonly RevisionGridControl _grid;
        private readonly AuthorRevisionHighlighting _authorHighlighting;

        public AuthorColumnProvider(RevisionGridControl grid, AuthorRevisionHighlighting authorHighlighting)
            : base("Author")
        {
            _grid = grid;
            _authorHighlighting = authorHighlighting;

            Column = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                HeaderText = "Author",
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                FillWeight = 80,
                Width = DpiUtil.Scale(150)
            };
        }

        public override void OnCellPainting(DataGridViewCellPaintingEventArgs e, GitRevision revision, (Brush backBrush, Color backColor, Color foreColor, Font normalFont, Font boldFont) style)
        {
            if (!revision.IsArtificial)
            {
                var font = _authorHighlighting.IsHighlighted(revision) ? style.boldFont : style.normalFont;

                _grid.DrawColumnText(e, (string)e.FormattedValue, font, style.foreColor, e.CellBounds.ReduceLeft(TextCellLeftMargin));
            }
        }

        public override void OnCellFormatting(DataGridViewCellFormattingEventArgs e, GitRevision revision)
        {
            e.Value = revision.Author ?? "";
            e.FormattingApplied = true;
        }

        public override bool TryGetToolTip(DataGridViewCellMouseEventArgs e, GitRevision revision, out string toolTip)
        {
            toolTip = default;
            return false;
        }
    }
}