import { render } from '../node_modules/lit-html/lit-html.js';

export function registar(context) {
    render(context.registar,document.getElementsByTagName('main')[0]);
}