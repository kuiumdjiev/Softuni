import { createFormObject } from '../helpers/FormatObject/createFormObject.js';
import { postJoin } from '../helpers/Post/post-join.js';
import { postLogin } from '../helpers/Post/post-login.js';
import { html } from '../node_modules/lit-html/lit-html.js';

let login = async e => {
    e.preventDefault();

    document.getElementById('err').style.display = 'none';
    let formObject = createFormObject(e.target)
    let email = formObject.email;
    let password = formObject.password;

    try {

        let body = {
            email,
            password,
        };
        postLogin(body);
    } catch (error) {
        document.getElementById('err').style.display = 'block';
    }



}


let approve = async e => {
    e.preventDefault();
    try {
        approveGuy(e.target.id);
    } catch (error) {
    }

}

let remove = async e => {
    e.preventDefault();
    try {
        removeGuy(e.target.id);
    } catch (error) {
    }

}

function join(id, e) {
    e.preventDefault();
    let body = {
        
        teamId: id
    };
    postJoin(body)
}


export let detailsTemplate = (data, user, pendings, members) => html`
<article class="layout">
    <img src="${data.logoUrl}" class="team-logo left-col">
    <div class="tm-preview">
        <h2>${data.name}</h2>
        <p>${data.description}</p>
        <span class="details">3 Members</span>
        <div>
            ${user === 'owner' ? html`<a href="/edit/${data._id}" class="action">Edit team</a>` : ''}

            ${user === 'nonMember' ? html`<a href="javascript:void(0)" @click=${(e) =>
        join(data._id, e)} class="action">Join team</a>` : ''}

            ${user === 'member' ? html`<a href="javascript:void(0)" @click=${(e)=>
                remove(sessionStorage.getItem('id'), e)} class="action invert">Leave team</a>` : ''}

            ${user === 'pending' ? html`Membership pending. <a @click=${(e)=> remove(sessionStorage.getItem('id'), e)}
                href="javascript:void(0)">Cancel request</a>` : ''}

        </div>
    </div>
    <div class="pad-large">
        <h3>Members</h3>
        <ul class="tm-members">
            ${members.map(x => memberTemplate(x, user))}
        </ul>
    </div>
    <div class="pad-large">
        <h3>Membership Requests</h3>
        <ul class="tm-members">
            ${pendings.map(x => pendingMembers(x, user))}
        </ul>
    </div>
</article>
`
let memberTemplate = (member, status) => html`
<li>
    ${member.user.username}
    ${status === 'owner' ? html`<a href="javascript:void(0)" @click=${(e)=> remove(member._id, e)} class="tm-control
        action">Remove from team</a>` : ''}
</li>`

let pendingMembers = (pending, status) => html`
<li>
    ${pending.user.username}
    ${status === 'owner' ? html`<a href="javascript:void(0)" @click=${(e)=> approve(pending._id, e)}
        class="tm-control action">Approve</a>` : ''}
    ${status === 'owner' ? html`<a href="javascript:void(0)" @click=${(e)=> remove(pending._id, e)} class="tm-control
        action">Decline</a>` : ''}
</li>`