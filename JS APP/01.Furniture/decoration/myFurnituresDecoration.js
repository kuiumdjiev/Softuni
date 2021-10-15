import { getMyFurnitures } from "../helpers/Get/get-myFurniture.js";
import { myFurnitureTemplate } from "../template/myFurnitureTemplate.js";

export async function myFurnitureDecoration(ctx, next) {
    let myFurniture=await getMyFurnitures(sessionStorage.getItem('id'))
    ctx.myFurniture  = myFurnitureTemplate(myFurniture);
    next();
}