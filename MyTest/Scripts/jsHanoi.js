/*js汉诺塔实现方法*/

function Disk(pnumber)
{
    this.Number = pnumber;
}

function Tower(name, count)
{
    var _tower = [];
    var _towerName = name;

    for (i = count; i > 0; i--) {
        var disk = new Disk(i);
        _tower.push(disk);
    }

    _tower.peek = function ()
    {
        return _tower[_tower.length - 1];
    }

    this.PopDisk = function ()
    {
        return _tower.pop();
    }

    this.PushDisk = function (disk)
    {
        if (_tower.length == 0) {
            _tower.push(disk);
        }
        else if (_tower.peek().Number <= disk.Number) {
            throw "error new disk"
        }
        else {
            _tower.push(disk);
        }
    }

    this.DiskCount = function ()
    {
        return _tower.length;
    }

    this.ToString = function ()
    {
        var result = _towerName + " ";
        for (i = _tower.length - 1; i >= 0; i--) {
            result = result + " " + _tower[i].Number;
        }
        //var div = document.createElement("div");
        $("#result").append("<div>" + result + "</div>");
    }
}

function 汉诺塔问题(diskCount)
{
    var _tower1 = new Tower("first", diskCount);
    var _tower2 = new Tower("second", 0);
    var _tower3 = new Tower("third", 0);

    this.Play = function ()
    {
        PrintTowerMessage();
        Play(_tower1, _tower3, _tower2, _tower1.DiskCount());
        PrintTowerMessage();
    }

    function Play(towerFrom, towerTo, towerHelp, count)
    {
        if (count > 1) {
            Play(towerFrom, towerHelp, towerTo, count - 1);
            towerTo.PushDisk(towerFrom.PopDisk());
            PrintTowerMessage();
            Play(towerHelp, towerTo, towerFrom, count - 1);
        }
        else {
            towerTo.PushDisk(towerFrom.PopDisk());
            PrintTowerMessage();
        }
    }

    function PrintTowerMessage()
    {
        _tower1.ToString();
        _tower2.ToString();
        _tower3.ToString();
        $("#result").append("<div style='color:red'>----------</div>");
    }
}

$(document).ready(function () { new 汉诺塔问题(4).Play(); });