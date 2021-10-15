import { render } from '../node_modules/lit-html/lit-html.js';

export function create(context) {
    render(context.create,document.getElementsByTagName('main')[0]);
}