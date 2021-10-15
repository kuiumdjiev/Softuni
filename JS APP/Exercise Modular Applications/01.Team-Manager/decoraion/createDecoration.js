import { createTemplate } from "../template/createTemplate.js";

export async function createDecoration(ctx, next) {
    ctx.create  = createTemplate();
    next();
}