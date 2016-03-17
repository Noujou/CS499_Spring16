using UnityEngine;
using System.Collections.Generic;
using System;

namespace CS499 {
    public class ColoringWall : MonoBehaviour
    {
        private static ColoringWall _inst;
        public static ColoringWall inst {
            get {
                if (_inst == null)
                    _inst = FindObjectOfType<ColoringWall>();
                return _inst;
            }
        }
        public Camera mainCamera;
        public Color fadeColor = Color.white;
        float wallDistence = 10;
        //students might want to access this but it should not show in unity inspector
        [HideInInspector]
        public Texture2D tex;
        Mesh mesh;
        Mesh mfMesh {
            get { return GetComponent<MeshFilter>().mesh; }
            set { GetComponent<MeshFilter>().mesh = value; }
        }
        //constance used for mesh gen;
        int xdiv = 64;
        int ydiv = 32;
        //Pixals per unity unit;
        float ppx, ppy;
        //vertical size, horizantal size is x2
        public int size = 128;
        int vtxPerLayer {get{ return(xdiv + 1); }}
        public ColorRules.Decay decayRule;
        public int _decayRuleIndex = 0;
        public int decayRuleIndex
        {
            get { return _decayRuleIndex; }
            set {
                _decayRuleIndex = value;
                decayRule = ColorRules.DecayRules[value];
            }
        }
        public ColorRules.ColorAdd colorAddRule;
        public int _colorAddRuleIndex = 0;
        public int colorAddRuleIndex {
            get { return _colorAddRuleIndex; }
            set {
                _colorAddRuleIndex = value;
                colorAddRule = ColorRules.ColorAddRules[value];
            }
        }
        void GenerateWall()
        {
            List<Vector3> vtx = new List<Vector3>();
            List<Vector3> nrm = new List<Vector3>();
            List<Vector2> uv0 = new List<Vector2>();
            List<int> tri = new List<int>();
            int layer = 0;
            for (float i = 0; i <= 1; i += 1f/ydiv)
            {
                for (float j = 0; j <= 1;j += 1f/xdiv) {
                    Vector3 sv = new Vector3(j,i,this.wallDistence);
                    Vector3 wv = mainCamera.ViewportToWorldPoint(sv);
                    int cur = vtx.Count;
                    //Debug.DrawLine(wv,mainCamera.transform.position, Color.blue, 10);
                    vtx.Add(wv);
                    nrm.Add((mainCamera.transform.position - wv).normalized);
                    uv0.Add(new Vector2(j,i));
                    if (j == 1) continue;//1 is exactly storably in float
                    if (i == 1) continue;
                    tri.AddRange(new int[] { cur, cur + vtxPerLayer, cur + 1 });
                    tri.AddRange(new int[] { cur+1, cur + vtxPerLayer, cur +vtxPerLayer+ 1 });
                }
                layer++;
            }
            mesh = new Mesh();
            mesh.vertices = vtx.ToArray();
            mesh.normals = nrm.ToArray();
            mesh.triangles = tri.ToArray();
            mesh.uv = uv0.ToArray();
            mfMesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
        // Use this for initialization
        void Start () {
            //texture to drawOn init
            tex = new Texture2D(2*size,size);
            Color[] c = tex.GetPixels();
            for (int i = 0; i < c.Length; i++)
                c[i] = fadeColor;
            tex.SetPixels(c);
            GetComponent<MeshRenderer>().materials[0].SetTexture("_MainTex", tex);
            //Generation of the mesh to fit perfectly in shot
            GenerateWall();
            //initation of our color mixer
            ColorRules.fadeColor = fadeColor;
            decayRuleIndex =  (int)ColorRules.DecayMap.None;
            colorAddRuleIndex = (int)ColorRules.ColorAddMap.Set;
            //calculate the pixals per inch. 
            Vector3 tp1 = mainCamera.ViewportToWorldPoint(Vector3.forward * this.wallDistence);
            Vector3 tp2 = mainCamera.ViewportToWorldPoint(Vector3.forward * this.wallDistence + Vector3.right+Vector3.up);
            Vector2 delta = (Vector2)(tp2 - tp1);
            ppx = tex.width / delta.x;
            ppy = tex.height / delta.y;
        }
        void SetColor(Vector3 pos, Color c)
        {
            Vector3 spos = mainCamera.WorldToViewportPoint(pos);
            int px = (int)(spos.x * tex.width);
            int py = (int)(spos.y * tex.height);
            applyAddRuleAt(px, py , c);
        }
        public void SetColor(Vector3 pos, Color c,float radius)
        {
            //To understand this function just think of what units things are in
            //radius is meters ppx and pixals per meter and texture is pixals
            
            //pos is in meters, spos is (screen percentage, (1,1,_) is the top right)
            Vector3 spos = mainCamera.WorldToViewportPoint(pos);
            //px and py are pixal x
            int px = (int)(spos.x * tex.width - radius*ppx);
            int py = (int)(spos.y * tex.height- radius*ppy);
            Func<float, float> sq = (x)=>x*x;
            for (int i = 0; i < 2 * radius * ppx; i++)
            {
                for (int j = 0; j < 2 * radius * ppy; j++)
                {
                    //if we are not in the circle skip this iteration
                    if (sq(i/ppx- radius) + sq(j/ppy - radius) > sq(radius)) continue;
                    applyAddRuleAt(px+i, py +j, c);
                }
            }

        }
        void applyAddRuleAt(int px, int py, Color c) {
            tex.SetPixel(px, py, colorAddRule(tex.GetPixel(px, py), c, Time.deltaTime * 4));
        }
        void HandleDecay() {
            Color[] arr = tex.GetPixels();
            for (int i = 0; i < arr.Length; i++) {
                arr[i] =decayRule(arr[i], Time.deltaTime);
            }
            tex.SetPixels(arr);
            tex.Apply();
        }
	    // Update is called once per frame
	    void Update () {
            HandleDecay();
            // the falling has cool effects try them out ^_^
            if (false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    colorAddRuleIndex = (colorAddRuleIndex + 1) % ColorRules.ColorAddRules.Length;
                    Debug.Log((ColorRules.ColorAddMap)colorAddRuleIndex);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    decayRuleIndex = (decayRuleIndex + 1) % ColorRules.DecayRules.Length;
                    Debug.Log((ColorRules.DecayMap)decayRuleIndex);
                }
            }
	    }
    }

    public abstract class ColorRules
    {
        public delegate Color Decay(Color cur, float dt);
        public delegate Color ColorAdd(Color a, Color b, float dt);
        public static Decay[] DecayRules = new Decay[] { DecayNone,DecayLinear, DecayGeometric };
        public enum DecayMap {None=0,Linear = 1,Geometric =2};
        public enum ColorAddMap {Set,Linear,Average,Geometric };
        public static ColorAdd[] ColorAddRules = new ColorAdd[] {ColorAddSet, ColorAddLinear,ColorAddAverage,ColorAddGeometric};
        public static Color fadeColor = Color.black;
        public static Color DecayNone(Color cur, float dt)
        {
            return cur;
        }
        public static Color DecayLinear(Color cur, float dt)
        {
            return new Color(
                    Mathf.MoveTowards(cur.r, fadeColor.r, dt),
                    Mathf.MoveTowards(cur.g, fadeColor.g, dt),
                    Mathf.MoveTowards(cur.b, fadeColor.b, dt)
                );
        }
        public static Color DecayGeometric(Color cur, float dt)
        {
            return cur * (1 - dt) + dt*fadeColor;
        }
        public static Color ColorAddSet(Color a, Color b,float dt)
        {
            return b;
        }
        public static Color ColorAddLinear(Color a, Color b,float dt)
        {
            return ColorAddLinear(a, b * dt);
        }
        public static Color ColorAddLinear(Color a, Color b)
        {
            return new Color(
                Mathf.Clamp01(a.r + b.r),
                Mathf.Clamp01(a.g + b.g),
                Mathf.Clamp01(a.b + b.b)
                );

        }
        public static Color ColorAddAverage(Color a, Color b,float dt )
        {
            //Debug.Log(""+ a * dt + b * (1 - dt)+(a * dt + b * (1 - dt)));
            return b*dt + a*(1-dt);

        }
        public static Color ColorAddGeometric(Color a, Color b, float dt)
        {
            Color tmp = (b * b*dt + a * a*(1-dt));
            return new Color(Mathf.Sqrt(tmp.r), Mathf.Sqrt(tmp.g), Mathf.Sqrt(tmp.b));
        }
    }
}