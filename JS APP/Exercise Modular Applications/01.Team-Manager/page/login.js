import { render } from '../node_modules/lit-html/lit-html.js';

export function login(context) {
    render(context.login,document.getElementsByTagName('main')[0]);
}