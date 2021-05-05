function scroll(index, rowHeight)
{
    var grid = document.getElementsByClassName("e-grid")[0];
    grid.querySelector('.e-content').scrollTop = (index - 1) * rowHeight;
}
