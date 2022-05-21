using UnityEngine;

public class CoordinateTranslator
{
     private Vector2 _fromMin;
     private Vector2 _fromMax;
     private Vector2 _fromMaxNormal;
     
     private Vector2 _toMin;
     private Vector2 _toMax;
     private Vector2 _toMaxNormal;

     private Vector2 _k;
     
     public CoordinateTranslator(Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax)
     {
          _fromMin = fromMin;
          _fromMax = fromMax;
          
          _toMin = toMin;
          _toMax = toMax;

          _fromMaxNormal = _fromMax - _fromMin;
          _toMaxNormal = _toMax - _toMin;

          _k = new Vector2(_toMaxNormal.x / _fromMaxNormal.x, _toMaxNormal.y / _fromMaxNormal.y);
     }

     public Vector2 Translate(Vector2 coords)
     {
          return new Vector2(
               TranslateCoordinate(coords.x, _fromMin.x, _k.x, _toMin.x),
               TranslateCoordinate(coords.y, _fromMin.y, _k.y, _toMin.y)
               );
     }

     public Vector3 TranslateToVector3WithZ(Vector2 coords)
     {
          return new Vector3(
               TranslateCoordinate(coords.x, _fromMin.x, _k.x, _toMin.x),
               0,
               TranslateCoordinate(coords.y, _fromMin.y, _k.y, _toMin.y)
               );
     }
     
     public Vector3 TranslateToVector3WithY(Vector2 coords)
     {
          return new Vector3(
               TranslateCoordinate(coords.x, _fromMin.x, _k.x, _toMin.x),
               TranslateCoordinate(coords.y, _fromMin.y, _k.y, _toMin.y),
               0
               );
     }

     private float TranslateCoordinate(float coordinate, float f0, float k, float t0) => (coordinate - f0) * k + t0;
}

