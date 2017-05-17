using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayersSorter {

    public List<SortingLayer> sortingLayers;

    public SortingLayersSorter(SortingLayer[] layers)
    {
        this.sortingLayers = new List<SortingLayer>(layers);
    }



    int compare(SpriteRenderer sr, SkeletonAnimation sa)
    {
        int srSortingIndex = findSortingLayerValue(sr);
        int saSortingIndex = findSortingLayerValue(sa);
        if (srSortingIndex - saSortingIndex != 0)
        {
            return srSortingIndex - saSortingIndex;
        }
        else
        {
            return sr.sortingOrder - sa.GetComponent<MeshRenderer>().sortingOrder;
        }
    }

    int compare(SpriteRenderer x, SpriteRenderer y)
    {
        int xSortingIndex = findSortingLayerValue(x);
        int ySortingIndex = findSortingLayerValue(y);
        if (xSortingIndex - ySortingIndex != 0)
        {
            return xSortingIndex - ySortingIndex;
        }
        else
        {
            return x.sortingOrder - y.sortingOrder;
        }
    }

    int compare(SkeletonAnimation x, SkeletonAnimation y)
    {
        int xSortingIndex = findSortingLayerValue(x);
        int ySortingIndex = findSortingLayerValue(y);
        if (xSortingIndex - ySortingIndex != 0)
        {
            return xSortingIndex - ySortingIndex;
        }
        else
        {
            return x.GetComponent<Renderer>().sortingOrder - y.GetComponent<Renderer>().sortingOrder;
        }
    }

    int compare(SkeletonAnimation sa, SpriteRenderer sr)
    {
        return -compare(sr, sa);
    }


    public int sortByLayers(object x, object y)
    {
        SpriteRenderer spriteX = x as SpriteRenderer;
        SpriteRenderer spriteY = y as SpriteRenderer;
        SkeletonAnimation skeletonX = x as SkeletonAnimation;
        SkeletonAnimation skeletonY = y as SkeletonAnimation;
        if (spriteX != null && spriteY != null)
        {
            return compare(spriteX, spriteY);
        }
        else if (spriteX != null && skeletonY != null)
        {
            return compare(spriteX, skeletonY);
        }
        else if (skeletonX != null && spriteY != null)
        {
            return compare(skeletonX, spriteY);
        }
        else if (skeletonX != null && skeletonY != null)
        {
            return compare(skeletonX, skeletonY);
        }
        else
        {
            return 0;
        }

    }

    int findSortingLayerValue(SpriteRenderer x)
    {
        foreach (SortingLayer sl in sortingLayers)
        {
            if (x.sortingLayerID.Equals(sl.id))
            {
                return sl.value;
            }
        }
        return 0;
    }

    int findSortingLayerValue(SkeletonAnimation x)
    {
        foreach (SortingLayer sl in sortingLayers)
        {
            Renderer m = x.GetComponent<Renderer>();
            if (m.sortingLayerID.Equals(sl.id))
            {
                return sl.value;
            }
        }
        return 0;
    }
}
