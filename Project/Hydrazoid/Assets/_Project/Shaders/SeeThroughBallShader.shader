Shader "Hydrazoid/SeeThroughBall"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }
        Pass{}
    }
}
