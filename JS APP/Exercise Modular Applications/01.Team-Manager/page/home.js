import { render } from '../node_modules/lit-html/lit-html.js';

export function home(context) {
    render(context.home,document.getElementsByTagName('main')[0]);
    render(context.nav,document.getElementsByTagName('nav')[0]);
}