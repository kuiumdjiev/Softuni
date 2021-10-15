import { getFurniture } from "../helpers/Get/get-furniture.js";
import { detailsFurnitureTemplate } from "../template/detailsTemplate.js";

export async function detailsFurnitureDecoration(ctx, next) {
    let id = ctx.params.id;
    let details =  await getFurniture(id)
    ctx.detailsFurniture  = detailsFurnitureTemplate(details);
    next();
}