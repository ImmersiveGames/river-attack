/// <summary>
/// Namespace:      General
/// Class:          Tools
/// Description:    Ferramentas de auxilio de funções estaticas
/// Author:         Renato Innocenti                    Date: 26/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v1.0           Date: 26/03/2018       Description: versão funcional
/// </summary>
///
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtils
{
    public static class Tools
    {
        public static List<T> ScriptableListToList<T>(List<int> listid, List<T> scriptableList) where T : ScriptableObject
        {
            List<T> defaultList = new List<T>();
            foreach (int item in listid)
            {
                foreach (T sitem in scriptableList)
                {
                    if (sitem.GetInstanceID() == item)
                    {
                        defaultList.Add(sitem);
                        break;
                    }
                }
            }

            return defaultList;
        }

        public static List<int> ListToScriptableList<T>(List<T> listProducts) where T : ScriptableObject
        {
            List<int> idlist = new List<int>();
            foreach (T item in listProducts)
            {
                idlist.Add(item.GetInstanceID());
            }
            return idlist;
        }

        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            var dst = destination.GetComponent(type) as T;
            if (!dst) dst = destination.AddComponent(type) as T;
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }
            return dst as T;
        }

        public static float RateWightList(float weight, float totalWeight)
        {
            return (weight / totalWeight) * 100;
        }
        public static string FirstLetterToUpper(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            s.ToLower();
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        public static float GetCento(float val, float max)
        {
            return (100 * val) / max;
        }
        public static float GetValorCento(float cento, float max)
        {
            return (max * cento) / 100;
        }

        public static bool IsBetween<T>(this T value, T minimum, T maximum) where T : IComparable<T>
        {
            if (value.CompareTo(minimum) < 0)
                return false;
            if (value.CompareTo(maximum) > 0)
                return false;
            return true;
        }
        /// <summary>
        /// Corrige o collider do objeto quando flipado
        /// </summary>
        /// <param name="go">o objeto que possui um collider flipado</param>
        public static void CorretBoxCollider(GameObject go)
        {
            if (go.GetComponent<Collider2D>())
            {
                Collider2D col = go.GetComponent<Collider2D>();
                SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();

                float newx = (sprite.flipX == true) ? -col.offset.x : col.offset.x;
                float newy = (sprite.flipY == true) ? -col.offset.y : col.offset.y;
                col.offset = new Vector2(newx, newy);
            }
        }
        /// <summary>
        /// Retorna um vetor2 com o formato em unity points da camera atual
        /// </summary>
        public static Vector2 CamSize
        {
            get
            {
                Camera cam = Camera.main;
                float height = 2f * cam.orthographicSize;
                float width = height * cam.aspect;
                return new Vector2(width, height);
            }
        }
        /// <summary>
        /// Cria um Mach a partir de um array de pontos de vetor2
        /// </summary>
        /// <param name="vertices2D">array com os pontos da mach a ser criada</param>
        /// <returns></returns>
        public static GameObject MashCreator(Vector2[] vertices2D)
        {
            Triangulator tr = new Triangulator(vertices2D);
            int[] indices = tr.Triangulate();

            // Create the Vector3 vertices
            Vector3[] vertices = new Vector3[vertices2D.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
            }
            // Create the mesh
            Mesh msh = new Mesh()
            {
                vertices = vertices,
                triangles = indices
            };
            msh.RecalculateNormals();
            msh.RecalculateBounds();
            // UV creator
            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
            }
            //Create Gameobject
            GameObject go = new GameObject();
            go.AddComponent(typeof(MeshRenderer));
            MeshFilter filter = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
            filter.mesh = msh;
            filter.mesh.uv = uvs;
            return go;
        }
        /// <summary>
        /// Cria uma lista de vertices com pontos flipados
        /// </summary>
        /// <param name="vertices2D">Conjunto de vertices a serem flipados</param>
        /// <param name="flipx">flipar na horizontal</param>
        /// <param name="flipy">flipar na vertical</param>
        /// <returns>uma nova lista com os vertices flipados</returns>
        public static List<Vector2> FlipVertices(List<Vector2> vertices2D, bool flipx = true, bool flipy = false)
        {
            List<Vector2> flip_vertices2D = new List<Vector2>();
            for (int i = 0; i < vertices2D.Count; i++)
            {
                float nx = (flipx) ? -vertices2D[i].x : vertices2D[i].x;
                float ny = (flipy) ? -vertices2D[i].y : vertices2D[i].y;
                flip_vertices2D.Add(new Vector2(nx, ny));
            }
            return flip_vertices2D;
        }

        public static Bounds GetChildRenderBounds(GameObject objeto, string tag)
        {
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            Renderer[] render = objeto.GetComponentsInChildren<Renderer>();
            if (render != null)
            {
                for (int i = 0; i < render.Length; i++)
                {
                    if (tag != null && !render[i].gameObject.CompareTag(tag)) continue;
                    //Debug.Log("Render: " + render[i].bounds);
                    bounds.Encapsulate(render[i].bounds);
                }
            }
            return bounds;
        }
 

        public static Bounds GetRenderBounds(GameObject objeto)
        {
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            Renderer render = objeto.GetComponent<Renderer>();
            if (render != null)
            {
                return render.bounds;
            }
            return bounds;
        }

        public static Bounds GetBounds(GameObject objeto, string tag = null)
        {
            Bounds bounds;
            Renderer childRender;
            bounds = GetRenderBounds(objeto);
            if (bounds.extents.x == 0)
            {
                bounds = new Bounds(objeto.transform.position, Vector3.zero);
                foreach (Transform child in objeto.transform)
                {
                    if (tag != null && !child.CompareTag(tag)) continue;
                    childRender = child.GetComponent<Renderer>();
                    if (childRender)
                    {
                        bounds.Encapsulate(childRender.bounds);
                    }
                    else
                    {
                        bounds.Encapsulate(GetBounds(child.gameObject));
                    }
                }
            }
            return bounds;
        }

        public static bool TryParseEnum<TEnum>(string aName, out TEnum aValue) where TEnum : struct
        {
            try
            {
                aValue = (TEnum)System.Enum.Parse(typeof(TEnum), aName);
                return true;
            }
            catch
            {
                aValue = default(TEnum);
                return false;
            }
        }

        public static void ToggleChildrens(Transform myTransform, bool setActive = true)
        {
            if (myTransform.childCount > 0)
            {
                for (int i = 0; i < myTransform.childCount; i++)
                {
                    myTransform.GetChild(i).gameObject.SetActive(setActive);
                }
            }
        }
        public static void TransformClear(Transform t)
        {
            foreach (Transform child in t)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
