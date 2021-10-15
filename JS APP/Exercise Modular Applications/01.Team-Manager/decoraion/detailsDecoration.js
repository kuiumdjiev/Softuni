import { getMembers } from "../helpers/Get/get-Members.js";
import { getTeam } from "../helpers/Get/get-Team.js";
import { detailsTemplate } from "../template/detailsTemplate.js";

export async function detailsDecoration(ctx, next) {
    let id = ctx.params.id;
    let team = await getTeam(id);
    let members = await getMembers(id);

    let status = '';

    if (sessionStorage.getItem('id') === team._ownerId) {
        status = 'owner';
    } else {
        let userMembership = members.find(x => x._id === sessionStorage.getItem('id'));
        if (userMembership === undefined) {
            status = 'nonMember';
        } else if (userMembership.status === 'pending') {
            status = 'pending'
        } else if (userMembership.status === 'member') {
            status = 'member';
        }
    }
    
        let pendings =  members.filter(x => x.status == 'pending');
        let member =  members.filter(x => x.status == 'member');
    
   

    ctx.details = detailsTemplate(team, status, pendings, member);
    next();
}