import { registrationTemplate } from "../template/registrationTemplate.js";

export async function registarDecoration(ctx, next) {
    ctx.register  = registrationTemplate();
    next();
}