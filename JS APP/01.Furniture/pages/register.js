import { render } from '../node_modules/lit-html/lit-html.js';

export function register(context) {
    render(context.register,document.getElementById('viewContainer'));
}