using ClienteExamen.Models;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClienteExamen.Repositories
{
    public class Repository
    {
        private String uriapi;
        private MediaTypeWithQualityHeaderValue headerjson;

        public Repository()
        {
            this.uriapi = "http://localhost:7590/";
            this.headerjson =new MediaTypeWithQualityHeaderValue("application/json");

        }

        public async Task<String> GetToken(String usuario
           , String password)
        {
            using (HttpClient client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
               {
                 new KeyValuePair<string, string>("grant_type", "password")
                ,new KeyValuePair<string, string>("username", usuario)
                ,new KeyValuePair<string, string>("password", password)
            });
                //ClienteExamen.Models.LoginModel login = new ClienteExamen.Models.LoginModel();
                //login.UserName = usuario;
                //login.Password = password;
                //String json = JsonConvert.SerializeObject(login);

        //        StringContent content =
        //new StringContent(json

        //, System.Text.Encoding.UTF8, "application/json");
                String peticion = "login";
                HttpResponseMessage response =
                    await client.PostAsync(peticion, content);
                if (response.IsSuccessStatusCode)
                {
                    String contenido =
                        await response.Content.ReadAsStringAsync();
                    var jObject = JObject.Parse(contenido);
                    return jObject.GetValue("access_token").ToString();
                }
                else
                {
                    return null;
                }
            }
        }


        //private async Task<T> CallApi<T>(String peticion
        //   , String token)
        //{
        //    using (HttpClient cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri(this.uriapi);
        //        cliente.DefaultRequestHeaders.Accept.Clear();
        //        cliente.DefaultRequestHeaders.Accept.Add(headerjson);
        //        if (token != null)
        //        {
        //            cliente.DefaultRequestHeaders.Add("Authorization", "bearer "
        //                + token);
        //        }
        //        HttpResponseMessage response =
        //            await cliente.GetAsync(peticion);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            T datos =
        //                await response.Content.ReadAsAsync<T>();
        //            return (T)Convert.ChangeType(datos, typeof(T));
        //        }
        //        else
        //        {
        //            return default(T);
        //        }
        //    }
        //}



        public async Task<Doctor> PerfilEmpleado(string token)
        {
           

            //Doctor empleado = await this.CallApi<Doctor>("api/PerfilEmpleado", token);

            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/PerfilEmpleado";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);

                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer "
                        + token);
                }


                HttpResponseMessage response = await
                    client.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    Doctor h =
                    await response.Content.ReadAsAsync<Doctor>();
                    return h;
                }
                else
                {
                    return null;
                }

            }


        }


        //-----------------

        public async Task<List<Hospital>> ListaHospitalesAsync(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/ListaHospitales";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);

                HttpResponseMessage response = await
                    client.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    List<Hospital> h =
                    await response.Content.ReadAsAsync<List<Hospital>>();
                    return h;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task InsertarHospitalAsync(Hospital hospital)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/InsertarHospital";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                Hospital dept = new Hospital();
                dept.Direccion = hospital.Direccion;
                dept.Hospital_cod = hospital.Hospital_cod;
                dept.NombreHospital = hospital.NombreHospital;
                dept.num_cama = hospital.num_cama;
                dept.Telefono = hospital.Telefono;

                String json = JsonConvert.SerializeObject(dept);
                StringContent content =
                    new StringContent(json
                    , System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(peticion, content);
                if (response.IsSuccessStatusCode)
                {

                }

            }
        }

        public async Task ModificarHospitalAsync(Hospital hospital, int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/ModificarHospital/" + id;
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                Hospital dept = new Hospital();
                dept.Direccion = hospital.Direccion;
                // dept.Hospital_cod = hospital.Hospital_cod;
                dept.NombreHospital = hospital.NombreHospital;
                dept.num_cama = hospital.num_cama;
                dept.Telefono = hospital.Telefono;

                String json = JsonConvert.SerializeObject(dept);

                StringContent content =
                    new StringContent(json
                    , System.Text.Encoding.UTF8, "application/json");

                await client.PutAsync(peticion, content);
            }
        }


        public async Task<Hospital> BuscarHospitalAsync(int num)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/BuscarHospital/" + num;
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                HttpResponseMessage response = await
                    client.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    Hospital departamento =
                    await response.Content.ReadAsAsync<Hospital>();
                    return departamento;
                }
                else
                {
                    return null;
                }
            }
        }

        //void EliminarHospital(int id);
        public async Task EliminarHospitalAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/EliminarHospital/" + id;
                //String peticion = "api/EliminarDatos/14/AAA";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                await client.DeleteAsync(peticion);
            }
        }


        //List<Mezcla> VerTodosDatos();
        public async Task<List<Mezcla>> VerTodosDatosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/VerTodosDatos";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                HttpResponseMessage response = await
                    client.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    List<Mezcla> h =
                    await response.Content.ReadAsAsync<List<Mezcla>>();
                    return h;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
