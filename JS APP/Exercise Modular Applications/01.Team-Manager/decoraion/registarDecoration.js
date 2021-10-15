import { registarTemplate } from "../template/registarTemplate.js";

export async function registarDecoration(ctx, next) {
    ctx.registar  = registarTemplate();
    next();
}