import { getFurniture } from "../helpers/Get/get-furniture.js";
import { updateFurnitureTemplate } from "../template/updateTemplate.js";

export async function updateFurnitureDecoration(ctx, next) {
    let id = ctx.params.id;
    let data =  await getFurniture(id)
    ctx.updateFurniture  = updateFurnitureTemplate(data);
    next();
}