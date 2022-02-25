import axios from 'axios';
import {FacultyRank} from '../common/types/FacultyRank'
import { FORMSTRUCTURE_BASE_URL, FORMCONTENT_BASE_URL, USER_BASE_URL, USER_MANAGEMENT_BASE_URL, FILE_UPLOAD_BASE_URL, REVIEW_BASE_URL } from '../js/constants';
import Group from '../models/BusinessObjects/Form/Group';
import IServerFormStructure from '../common/types/IServerFormStructure'
import IServerFormContent from '../common/types/IServerFormContent'
import IUser from '../models/Users/IUser';
import UserType from '../models/Users/UserType';


const FORM_STRUCTURE_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + FORMSTRUCTURE_BASE_URL + "/";
const FORM_CONTENT_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + FORMCONTENT_BASE_URL + "/";
const USERS_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + USER_BASE_URL + "/";
const REVIEW_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + REVIEW_BASE_URL + "/";
const USER_MANAGEMENT_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + USER_MANAGEMENT_BASE_URL + "/";
const UPLOAD_FILE_URL = process.env.REACT_APP_SERVER_BASE_URL + "/" + FILE_UPLOAD_BASE_URL + "/"


export async function GetFormStructureById(formId: string){
    const GET_URL = FORM_STRUCTURE_URL + formId;
    return axios.get(GET_URL);
}

export function GetFormStructureByClassification(formYear: string, facultyRank: FacultyRank){
    const GET_URL = FORM_STRUCTURE_URL + formYear + "/" + facultyRank;
    return axios.get(GET_URL);
}

export function UpdateFormStructure(formId: string, structure: IServerFormStructure)
{
    const UPDATE_URL = FORM_STRUCTURE_URL + formId;
    return axios.patch(UPDATE_URL, structure);
}

export function CreateFormStructure(formYear: string, facultyRank: FacultyRank, groups: Array<Group>)
{
    const CREATE_URL = FORM_STRUCTURE_URL + formYear + "/" + facultyRank;
    return axios.post(CREATE_URL, groups);
}

export function DeleteFormStructure(formId: string){
    const DELETE_URL = FORM_STRUCTURE_URL + formId;
    return axios.delete(DELETE_URL);
}

export function GetFormContentById(formId: string, facultyId: string){
    const GET_URL = FORM_CONTENT_URL + facultyId + "/" + formId;
    return axios.get(GET_URL);
}

export function GetFormStubs(facultyId: string){
    const GET_URL = FORM_CONTENT_URL + facultyId + '/all'
    return axios.get(GET_URL);
}

export async function UpdateFormContent(formId: string, facultyId: string, formContent: IServerFormContent)
{
    const UPDATE_URL = FORM_CONTENT_URL + facultyId + "/" + formId;
    return axios.patch(UPDATE_URL, formContent);
}

export function CreateFormContent(formContent: IServerFormContent, facultyId: string)
{
    const CREATE_URL = FORM_CONTENT_URL + facultyId;
    return axios.post(CREATE_URL, formContent);
}

export function DeleteFormContent(formId: string, facultyId: string){
    const DELETE_URL = FORM_CONTENT_URL + facultyId + "/" + formId;
    return axios.delete(DELETE_URL);
}

export function GetReviewerByFaculty(facultyId: string, userType: UserType) {
    const GET_URL = USERS_URL + facultyId + "/" + userType;
    return axios.get(GET_URL);
}

export async function GetUsersByEmail(email: string){
    const GET_USERS_URL = USERS_URL + email;
    return axios.get(GET_USERS_URL);
}

export async function GetUsers(){
    const GET_USERS_URL = USER_MANAGEMENT_URL + '/All';
    return axios.get(GET_USERS_URL);

}

export async function CreateUser(user: IUser){
    const CREATE_USER_URL = USER_MANAGEMENT_URL;
    return axios.post(CREATE_USER_URL, user);

}

export async function DeleteUser(userId: string){
    const DELETE_USER_URL = USER_MANAGEMENT_URL + "/" + userId;
    return axios.delete(DELETE_USER_URL);

}

export async function UpdateUser(user: IUser){
    const UPDATE_USER_URL = USER_MANAGEMENT_URL;
    return axios.patch(UPDATE_USER_URL, user);

}

export async function UploadUserFile() {
    const UPLOAD_FILE = UPLOAD_FILE_URL + "/users"
    return axios.post(UPLOAD_FILE)

}

export async function UploadSpotScores() {
    const UPLOAD_FILE = UPLOAD_FILE_URL + "/scores"
    return axios.post(UPLOAD_FILE)

}

export async function GetReviewerFormStubs(reviewer: IUser) {
    const GET_URL = REVIEW_URL + reviewer.id;
    return axios.get(GET_URL)
}