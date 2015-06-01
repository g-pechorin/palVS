# Visual Studio Plugin

I got mad that I couldn't work with V$ in the ways I wanted to.
So I've made [a V$ plugin](https://github.com/g-pechorin/palVS/raw/master/dist/palVSNPpp.vsix) to;
	* open the active file in [NotePad++](https://notepad-plus-plus.org/)
		* `CTRL` + `ALT` + `SHIFT` and `O`
	* open the active file's project in [Tortoise HG](http://tortoisehg.bitbucket.org/)
		* `CTRL` + `ALT` + `SHIFT` and `M`
		* (You'll need `thg.exe` on your `PATH`)

It was made with / used by [Visual Studio Community 2013](https://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx).
If you want to this project it you'll need the [Visual Studio SDK](https://www.microsoft.com/en-gb/download/details.aspx?id=40758).
I'd also advocate that you use [EditorConfig](http://editorconfig.org/) with their [plugin for Visual Studio](https://github.com/editorconfig/editorconfig-visualstudio#readme)

(... and you'll need to setup/provide your own `.snk` file)

# License

	palVS - Visual Studio Extensions
    Copyright (C) 2015 Peter LaValle

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.