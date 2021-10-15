import { render } from '../node_modules/lit-html/lit-html.js';

export function details(context) {
    render(context.details,document.getElementsByTagName('main')[0]);
}