import { createFurnitureTemplate } from "../template/createTemplate.js";

export async function createFurnitureDecoration(ctx, next) {
    ctx.createFurniture  = createFurnitureTemplate();
    next();
}