import { loginTemplate } from "../template/loginTemplate.js";

export async function loginDecoration(ctx, next) {
    ctx.login  = loginTemplate();
    next();
}