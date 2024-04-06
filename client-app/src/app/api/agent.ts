import axios, { AxiosResponse } from 'axios';
import { Activity } from '../layout/models/activity';

// const sleep = (delay: number) => {
//   return new Promise(resolve => setTimeout(resolve, delay));
// };

axios.defaults.baseURL = 'http://localhost:5000/api';

// Axios interceptors allow you to do something when receiving a response
// In this case we are delaying the response.
// axios.interceptors.response.use(async response => {
//   try {
//     await sleep(1000);
//     return response;
//   } catch (err) {
//     console.log(err);
//     return await Promise.reject(err);
//   }
// });

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: object) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: object) =>
    axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Activities = {
  list: () => requests.get<Activity[]>('/activities'),
  details: (id: string) => requests.get<Activity>(`/activities/${id}`),
  create: (activity: Activity) => requests.post<void>('/activities', activity),
  update: (activity: Activity) =>
    requests.put<void>(`/activities/${activity.id}`, activity),
  delete: (id: string) => requests.del<void>(`/activites/${id}`),
};

const agent = {
  Activities,
};

export default agent;
