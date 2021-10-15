import { render } from '../node_modules/lit-html/lit-html.js';

export function browse(context) {
    render(context.browse,document.getElementsByTagName('main')[0]);
}